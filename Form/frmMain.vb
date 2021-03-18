'Imports LeaveFilingSystem.dsLeaveFilingTableAdapters
'Imports LeaveFilingSystem.dsLeaveFiling
'Imports LeaveFilingSystem.dsJeonsoftTableAdapters
Imports LeaveFilingSystem.dsJeonsoft
Imports System.Data.SqlClient
Imports BlackCoffeeLibrary.BlackCoffee

Public Class frmMain
    Private clsConnection As New clsConnection
    Private clsMain As New Main
    Private clsDbMethod As New SqlDbMethod(clsConnection.JeonsoftConnection)

    Private employeeId As Integer = 0
    Private employeeCode As String = String.Empty
    Private employeeName As String = String.Empty
    Private positionId As Integer = 0
    Private positionName As String = String.Empty
    Private departmentId As Integer = 0
    Private departmentName As String = String.Empty
    Private employmentTypeId As Integer = 0
    Private employmentType As String = String.Empty
    Private teamId As Integer = 0
    Private teamName As String = String.Empty
    Private dateHired As DateTime = Nothing
    Private dateRegular As DateTime = Nothing

    Private dsJeonsoft As New dsJeonsoft

    Public Sub New(ByVal _employeeId As Integer, ByVal _employeeCode As String, ByVal _employeeName As String, ByVal _positionId As Integer, ByVal _positionName As String, ByVal _departmentId As Integer, ByVal _departmentName As String, ByVal _teamName As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        employeeId = _employeeId
        employeeCode = _employeeCode
        employeeName = _employeeName
        positionId = _positionId
        positionName = _positionName
        departmentId = _departmentId
        departmentName = _departmentName
        teamName = _teamName

        Dim _paramTeamName(0) As SqlParameter
        _paramTeamName(0) = New SqlParameter("@Name", SqlDbType.VarChar)
        _paramTeamName(0).Value = teamName.ToString.Trim

        Dim _reader As IDataReader = clsDbMethod.ExecuteReader("SELECT Id FROM tblTeams WHERE (TRIM(Name) = @Name)", CommandType.Text, _paramTeamName)
        While _reader.Read
            teamId = _reader.Item("Id")
        End While
        _reader.Close()

        UsernameToolStripMenuItem.Text = "  " & StrConv(employeeName, VbStrConv.ProperCase)
        UserItemToolStripMenuItem.Text = teamName & " " & positionName

        If departmentName.Equals(teamName) Then
            DepartmentToolStripStatusLabel.Text = departmentName
            SectionToolStripStatusLabel.Text = String.Empty
        Else
            DepartmentToolStripStatusLabel.Text = " " & departmentName & "  |"
            SectionToolStripStatusLabel.Text = teamName
            UserItemToolStripMenuItem.Text = positionName
        End If

        tmrMain.Start()

        clsMain.FormLoader(Me, New frmLeaveList(employeeId, positionId, departmentId, teamId, employmentTypeId))
    End Sub

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'disable the resize/maximize button of the form if maximize, enable if the form is minimize
        AddHandler Me.SizeChanged, AddressOf frmMain_SizeEventHandler

        'disable resize/maximize button of the form
        Me.MaximizeBox = False
    End Sub

    Private Sub frmMain_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        tmrMain.Stop()
        Application.Exit()
    End Sub

    Private Sub tmrMain_Tick(sender As Object, e As EventArgs) Handles tmrMain.Tick
        DatetimeToolStripMenuItem.Text = DateTime.Now.ToString("MMMM dd, yyyy")
    End Sub

    Private Sub LogOutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LogOutToolStripMenuItem.Click
        Me.Hide()
        frmLogin.Show()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Application.Exit()
    End Sub

    'file
    Private Sub LeaveFilingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LeaveFilingToolStripMenuItem.Click
        clsMain.FormLoader(Me, New frmLeaveList(employeeId, positionId, departmentId, teamId, employmentTypeId))
    End Sub

    Private Sub LeaveApprovalToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub

    'prevent form resizing when double clicked the titlebar or dragged
    Protected Overloads Overrides Sub WndProc(ByRef m As Message)
        Const WM_NCLBUTTONDBLCLK As Integer = 163 'define doubleclick event
        Const WM_NCLBUTTONDOWN As Integer = 161 'define leftbuttondown event
        Const WM_SYSCOMMAND As Integer = 274 'define move action
        Const HTCAPTION As Integer = 2 'define that the WM_NCLBUTTONDOWN is at titlebar
        Const SC_MOVE As Integer = 61456 'trap move action
        'disable moving titleBar
        If (m.Msg = WM_SYSCOMMAND) AndAlso (m.WParam.ToInt32() = SC_MOVE) Then
            Exit Sub
        End If
        'track whether clicked on title bar
        If (m.Msg = WM_NCLBUTTONDOWN) AndAlso (m.WParam.ToInt32() = HTCAPTION) Then
            Exit Sub
        End If
        'disable double click on title bar
        If (m.Msg = WM_NCLBUTTONDBLCLK) Then
            Exit Sub
        End If

        MyBase.WndProc(m)
    End Sub

    Private Sub frmMain_SizeEventHandler(ByVal sender As Object, ByVal e As EventArgs)
        If Me.WindowState = FormWindowState.Minimized Then
            Me.MaximizeBox = True

        ElseIf Me.WindowState = FormWindowState.Maximized Then
            Me.MaximizeBox = False
        End If
    End Sub

End Class