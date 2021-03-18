Imports System.Data.SqlClient
Imports BlackCoffeeLibrary.BlackCoffee

Public Class frmLogin
    Private clsMethod As New Main
    Private clsConnection As New clsConnection
    Private clsDbMethod As New SqlDbMethod(clsConnection.JeonsoftConnection)

    Private employeeId As Integer = 0
    Private employeeCode As String = String.Empty
    Private employeeName As String = String.Empty
    Private positionId As Integer = 0
    Private positionName As String = String.Empty
    Private departmentId As Integer = 0
    Private departmentName As String = String.Empty
    Private teamName As String = String.Empty

    Private count As Integer = 0

    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ActiveControl = txtEmployeeId

        'sir alvin
        'txtEmployeeId.Text = "1705-025"

        'sir shimamura
        'txtEmployeeId.Text = "1803-000"

        'sir clark
        'txtEmployeeId.Text = "1709-006"

        'sir emman
        'txtEmployeeId.Text = "1701-066"

        'sir harry
        'txtEmployeeId.Text = "1807-002"

        'sir noriel
        'txtEmployeeId.Text = "1901-033"

        'sir jhon
        'txtEmployeeId.Text = "1811-031"

        'sir tony
        'txtEmployeeId.Text = "1605-002"

        'mam liza
        'txtEmployeeId.Text = "2009-015"

        'jen 
        'txtEmployeeId.Text = "1910-020"

        'operator
        'txtEmployeeId.Text = "1811-021"

        'line leader
        'txtEmployeeId.Text = "1705-022"

        'sir oliver
        'txtEmployeeId.Text = "1511-002"

        'mam donna
        'txtEmployeeId.Text = "1907-002"

        'nurse
        txtEmployeeId.Text = "1805-003"

        'sir eldrin
        'txtEmployeeId.Text = "2005-002"

        'cheenee
        'txtEmployeeId.Text = "1708-001"

        'me
        'txtEmployeeId.Text = "2009-002"
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If String.IsNullOrEmpty(txtEmployeeId.Text.Trim) Then
            MessageBox.Show("Please enter your employee ID.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtEmployeeId.Focus()
            Return
        End If

        Dim _paramEmployeeCode1(0) As SqlParameter
        _paramEmployeeCode1(0) = New SqlParameter("@EmployeeCode", SqlDbType.VarChar)
        _paramEmployeeCode1(0).Value = txtEmployeeId.Text.Trim

        count = clsDbMethod.ExecuteScalar("SELECT COUNT(Id) FROM dbo.viwEmployeeInfo WHERE (EmployeeCode = @EmployeeCode)", CommandType.Text, _paramEmployeeCode1)

        If count > 0 Then
            Dim _paramEmployeeCode2(0) As SqlParameter
            _paramEmployeeCode2(0) = New SqlParameter("@EmployeeCode", SqlDbType.VarChar)
            _paramEmployeeCode2(0).Value = txtEmployeeId.Text.Trim

            Dim _reader As IDataReader = clsDbMethod.ExecuteReader("SELECT Id, EmployeeCode, TRIM(FirstName) AS FirstName, TRIM(MiddleName) AS MiddleName, TRIM(LastName) AS LastName, PositionId, Position, DepartmentId, Department, Team FROM dbo.viwGroupEmployees WHERE (EmployeeCode = @EmployeeCode)", CommandType.Text, _paramEmployeeCode2)

            While _reader.Read
                employeeId = _reader.Item("Id")
                employeeCode = _reader.Item("EmployeeCode").ToString.Trim

                'handle not formatted or null middle name e.g. japanese have no middle name
                If _reader.Item("MiddleName").ToString.Trim.Equals("-") Or _reader.Item("MiddleName") Is DBNull.Value Then
                    employeeName = _reader.Item("FirstName").ToString.Trim & " " & _reader.Item("LastName").ToString.Trim
                Else
                    employeeName = _reader.Item("FirstName").ToString.Trim & " " & _reader.Item("MiddleName").ToString.Trim & " " & _reader.Item("LastName").ToString.Trim
                End If

                positionId = _reader.Item("PositionId")
                positionName = _reader.Item("Position").ToString.Trim
                departmentId = _reader.Item("DepartmentId").ToString.Trim
                departmentName = _reader.Item("Department").ToString.Trim
                teamName = _reader.Item("Team").ToString.Trim
            End While
            _reader.Close()

            Me.Hide()
            Dim _frmMain As New frmMain(employeeId, employeeCode, employeeName, positionId, positionName, departmentId, departmentName, teamName)
            _frmMain.Show()
            txtEmployeeId.Clear()
        Else
            MessageBox.Show("Invalid employee ID.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtEmployeeId.Focus()
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Application.Exit()
    End Sub

End Class