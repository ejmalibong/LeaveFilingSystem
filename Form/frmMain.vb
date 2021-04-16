Imports System.Data.SqlClient
Imports BlackCoffeeLibrary.BlackCoffee
Imports LeaveFilingSystem
Imports LeaveFilingSystem.dsLeaveFiling
Imports LeaveFilingSystem.dsLeaveFilingTableAdapters
Imports System.ComponentModel
Imports System.Net.Mail

Public Class frmMain
    Private connection As New clsConnection
    Private dbJeonsoft As New SqlDbMethod(connection.JeonsoftConnection)
    Private dbLeaveFiling As New SqlDbMethod(connection.LocalConnection)
    Private main As New Main
    'server datetime
    Private serverDate As DateTime = dbLeaveFiling.GetServerDate
    'constructors
    Private employeeId As Integer = 0
    Private employeeCode As String = String.Empty
    Private employeeName As String = String.Empty
    Private positionId As Integer = 0
    Private positionName As String = String.Empty
    Private departmentId As Integer = 0
    Private departmentName As String = String.Empty
    Private teamId As String = 0
    Private teamName As String = String.Empty
    Private employmentTypeId As Integer = 0
    Private employmentTypeName As String = String.Empty
    Private emailAddress As String = String.Empty
    Private mobileNumber As String = String.Empty
    Private nbcEmailAddress As String = String.Empty

    Private Shared mailSent As Boolean = False

    Public Sub New(ByVal _employeeId As Integer, ByVal _employeeCode As String, ByVal _employeeName As String, ByVal _positionId As Integer, ByVal _positionName As String, ByVal _departmentId As Integer, ByVal _departmentName As String, ByVal _teamId As Integer, ByVal _teamName As String, ByVal _employmentTypeId As Integer, ByVal _employmentTypeName As String, ByVal _emailAddress As String, ByVal _mobileNumber As String, ByVal _nbcEmailAddress As String)

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
        teamId = _teamId
        teamName = _teamName
        employmentTypeId = _employmentTypeId
        employmentTypeName = _employmentTypeName
        emailAddress = _emailAddress
        mobileNumber = _mobileNumber
        nbcEmailAddress = _nbcEmailAddress

        UsernameToolStripMenuItem.Text = "  " & StrConv(employeeName, VbStrConv.ProperCase)
        UserItemToolStripMenuItem.Text = teamName & " " & positionName

        If departmentName.Equals(teamName) Then
            DepartmentToolStripStatusLabel.Text = departmentName
            SectionToolStripStatusLabel.Text = String.Empty
        Else
            If String.IsNullOrEmpty(teamName) Then
                DepartmentToolStripStatusLabel.Text = " " & departmentName
            Else
                DepartmentToolStripStatusLabel.Text = " " & departmentName & "  |"
            End If

            SectionToolStripStatusLabel.Text = teamName
            UserItemToolStripMenuItem.Text = positionName
        End If

        tmrMain.Start()

        If teamId = 1 Then
            HrListToolStripMenuItem.Visible = True
            main.FormLoader(Me, New frmHrRecord(employeeId, positionId, departmentId, teamId, employmentTypeId))
        Else
            HrListToolStripMenuItem.Visible = False
            main.FormLoader(Me, New frmLeaveRecord(employeeId, positionId, departmentId, teamId, employmentTypeId))
        End If
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

    Private Sub LeaveFilingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LeaveFilingToolStripMenuItem.Click
        main.FormLoader(Me, New frmLeaveRecord(employeeId, positionId, departmentId, teamId, employmentTypeId))
    End Sub

    Private Sub HrListToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HrListToolStripMenuItem.Click
        main.FormLoader(Me, New frmHrRecord(employeeId, positionId, departmentId, teamId, employmentTypeId))
    End Sub

    Private Sub LogOutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LogOutToolStripMenuItem.Click
        Me.Hide()
        frmLogin.Show()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Application.Exit()
    End Sub

#Region "Sub"
    'prevent form resizing when double clicked the titlebar or dragged
    Protected Overloads Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
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

    Public Sub SendEmailRequestor(ByVal _isApproved As Boolean, _employeeId As Integer, ByVal _leaveType As String, ByVal _startDate As Date, ByVal _endDate As Date)
        Try
            Dim client As New SmtpClient()
            Dim message As New MailMessage()
            Dim messageBody As String = "<font size=""5"" face=""Segoe UI"" color=""black"">" & _
                "Good day! <br> <br> "
            
            message.From = New MailAddress("nbcleaveapplication@gmail.com", "NBC Leave Application")

            Dim _prmRequestor(0) As SqlParameter
            _prmRequestor(0) = New SqlParameter("@EmployeeId", SqlDbType.Int)
            _prmRequestor(0).Value = _employeeId

            Dim _reader As IDataReader = dbJeonsoft.ExecuteReader("SELECT TRIM(EmailAddress) AS EmailAddress FROM dbo.tblEmployees WHERE Id = @EmployeeId", CommandType.Text, _prmRequestor)

            While _reader.Read
                If _reader.Item("EmailAddress") Is DBNull.Value Then
                    message.To.Add("it1@nbc-p.com")
                Else
                    message.To.Add(_reader.Item("EmailAddress").ToString.Trim)
                    'message.To.Add("it1@nbc-p.com")
                End If
            End While
            _reader.Close()

            message.Subject = "Leave Notification"
            message.IsBodyHtml = True

            If _isApproved = True Then
                If _startDate.Date.Date.Equals(_endDate.Date.Date) Then
                    messageBody += "Your " & _leaveType.ToString.Trim & " dated " & _startDate.Date.ToString("MMMM dd, yyyy") & " is approved. <br> Thank you. <br> <br>" & _
                        "NOTE: This is a system-generated email. Please do not reply."
                Else
                    messageBody += "Your " & _leaveType.ToString.Trim & " dated from " & _startDate.Date.ToString("MMMM dd, yyyy") & " to " & _
                        _endDate.Date.ToString("MMMM dd, yyyy") & "is approved. <br> Thank you. <br> <br>" & _
                        "NOTE: This is a system-generated email. Please do not reply."
                End If

            Else
                If _startDate.Date.Date.Equals(_endDate.Date.Date) Then
                    messageBody += "Your " & _leaveType.ToString.Trim & " dated " & _startDate.Date.ToString("MMMM dd, yyyy") & " is disapproved. <br> <br>" & _
                        "NOTE: This is a system-generated email. Please do not reply."
                Else
                    messageBody += "Your " & _leaveType.ToString.Trim & " dated from " & _startDate.Date.ToString("MMMM dd, yyyy") & " to " & _
                        _endDate.Date.ToString("MMMM dd, yyyy") & "is disapproved. <br> <br>" & _
                        "NOTE: This is a system-generated email. Please do not reply."
                End If
            End If

            message.Body = messageBody

            client.Host = "smtp.gmail.com"
            client.Port = 587
            client.EnableSsl = True
            client.UseDefaultCredentials = False
            client.Credentials = New Net.NetworkCredential("nbcleaveapplication@gmail.com", "qwerty123$$")

            Dim userState As String = "userState"
            AddHandler client.SendCompleted, AddressOf SendCompletedCallback

            client.SendAsync(message, userState)

            StatusToolStripStatusLabel.Visible = True
            StatusToolStripStatusLabel.Text = "Sending email. Please wait......"
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Public Sub SendEmailRequestors(ByVal _isNoEmail As Boolean, ByVal _recipient As String, ByVal _message As String)
    '    Dim client As New SmtpClient()
    '    Dim message As New MailMessage()

    '    'If _isNoEmail = True Then
    '    '    _recipient = "it1@nbc-p.com"
    '    'End If

    '    message.From = New MailAddress("nbcleaveapplication@gmail.com", "NBC Leave Application")
    '    'message.To.Add(_recipient)
    '    message.To.Add("it1@nbc-p.com")
    '    message.Subject = "Leave Notification"
    '    message.Body = _message

    '    client.Host = "smtp.gmail.com"
    '    client.Port = 587
    '    client.EnableSsl = True
    '    client.UseDefaultCredentials = False
    '    client.Credentials = New Net.NetworkCredential("nbcleaveapplication@gmail.com", "qwerty123$$")

    '    Dim userState As String = "userState"
    '    AddHandler client.SendCompleted, AddressOf SendCompletedCallback

    '    client.SendAsync(message, userState)

    '    StatusToolStripStatusLabel.Visible = True
    '    StatusToolStripStatusLabel.Text = "Sending email. Please wait......"
    'End Sub

    Public Sub SendEmailApprovers(ByVal _employeeId As Integer, ByVal _leaveType As String, ByVal _employeeName As String, ByVal _department As String, ByVal _date As String, ByVal _reason As String)
        Try
            Dim client As New SmtpClient()
            Dim message As New MailMessage()
            Dim messageBody As String = "<font size=""5"" face=""Segoe UI"" color=""black"">" & _
                                        "Good day! <br> <br> " & _
                                        "New leave application for your approval. Please check the information below for your reference. <br> <br> " & _
                                        "&nbsp;Leave Type: " & _leaveType.ToString.Trim & " <br> " & _
                                        "&nbsp;Employee Name: " & _employeeName.ToString.Trim & " <br> " & _
                                        "&nbsp;Department/Section: " & _department.ToString.Trim & " <br> " & _
                                        "&nbsp;Date: " & _date.ToString.Trim & " <br> " & _
                                        "&nbsp;Reason: " & _reason.ToString.Trim & _
                                        "<br>" & "<br>" & _
                                        "Please check on your Leave Application System." & _
                                        "<br>" & "<br>" & _
                                        "If you have any concern, please call IT (Local 232). <br> <br>" & _
                                        "Thank you."

            message.From = New MailAddress("nbcleaveapplication@gmail.com", "NBC Leave Application")

            Dim _prmApprover(0) As SqlParameter
            _prmApprover(0) = New SqlParameter("@EmployeeId", SqlDbType.Int)
            _prmApprover(0).Value = _employeeId

            Dim _reader As IDataReader = dbLeaveFiling.ExecuteReader("SELECT TRIM(EmailAddress) AS EmailAddress FROM dbo.EmployeeEmail WHERE EmployeeId = @EmployeeId", CommandType.Text, _prmApprover)

            While _reader.Read
                If _reader.Item("EmailAddress") Is DBNull.Value Then
                    message.To.Add("it1@nbc-p.com")
                Else
                    message.To.Add(_reader.Item("EmailAddress").ToString.Trim)
                    'message.To.Add("it1@nbc-p.com")
                End If
            End While
            _reader.Close()

            message.Subject = "Leave Notification"
            message.IsBodyHtml = True
            message.Body = messageBody

            client.Host = "smtp.gmail.com"
            client.Port = 587
            client.EnableSsl = True
            client.UseDefaultCredentials = False
            client.Credentials = New Net.NetworkCredential("nbcleaveapplication@gmail.com", "qwerty123$$")

            Dim userState As String = "userState"
            AddHandler client.SendCompleted, AddressOf SendCompletedCallback

            client.SendAsync(message, userState)

            StatusToolStripStatusLabel.Visible = True
            StatusToolStripStatusLabel.Text = "Sending email. Please wait......"
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Async Sub SendCompletedCallback(ByVal sender As Object, ByVal e As AsyncCompletedEventArgs)
        Try
            Dim token As String = CStr(e.UserState)

            If e.Cancelled Then
                StatusToolStripStatusLabel.Text = "[" & token & "] Send canceled."
            End If

            If e.Error IsNot Nothing Then
                StatusToolStripStatusLabel.Text = "[" & token & "] " & e.Error.ToString & ""
            Else
                StatusToolStripStatusLabel.Text = "Email sent. Thank you."
            End If

            Await HideStatus()

            mailSent = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Async Function HideStatus() As Task(Of Boolean)
        Await Task.Delay(2000)
        StatusToolStripStatusLabel.Visible = False
        Return True
    End Function
#End Region

End Class