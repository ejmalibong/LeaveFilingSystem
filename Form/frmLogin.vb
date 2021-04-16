Imports System.Data.SqlClient
Imports BlackCoffeeLibrary.BlackCoffee

Public Class frmLogin
    Private connection As New clsConnection
    Private dbLeaveFiling As New SqlDbMethod(connection.LocalConnection)
    Private main As New Main

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

    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
        'txtEmployeeId.Text = "1805-003"

        'sir eldrin
        'txtEmployeeId.Text = "2005-002"

        'mam cheenee
        'txtEmployeeId.Text = "1708-001"

        'mam wella
        'txtEmployeeId.Text = "1709-001"

        'mam cath
        'txtEmployeeId.Text = "1802-001"

        'mam mj
        'txtEmployeeId.Text = "1701-075"

        'mam meds
        'txtEmployeeId.Text = "2006-021"

        'mam gavileno
        'txtEmployeeId.Text = "2103-000"

        'mam dette
        'txtEmployeeId.Text = "1503-001"

        'me
        'txtEmployeeId.Text = "2009-002"

        'sir vincent
        'txtEmployeeId.Text = "2011-002"

        Application.EnableVisualStyles()
        Me.ActiveControl = txtEmployeeId
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If String.IsNullOrEmpty(txtEmployeeId.Text.Trim) Then
            MessageBox.Show("Please enter your employee ID.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtEmployeeId.Focus()
            Return
        End If

        Dim _count As Integer = 0
        Dim _prmEmpCode1(0) As SqlParameter
        _prmEmpCode1(0) = New SqlParameter("@EmployeeCode", SqlDbType.VarChar)
        _prmEmpCode1(0).Value = txtEmployeeId.Text.Trim

        _count = dbLeaveFiling.ExecuteScalar("RdEmployee", CommandType.StoredProcedure, _prmEmpCode1)

        If _count > 0 Then
            Dim _prmEmpCode2(0) As SqlParameter
            _prmEmpCode2(0) = New SqlParameter("@EmployeeCode", SqlDbType.VarChar)
            _prmEmpCode2(0).Value = txtEmployeeId.Text.Trim

            Dim _reader As IDataReader = dbLeaveFiling.ExecuteReader("RdEmployee", CommandType.StoredProcedure, _prmEmpCode2)

            While _reader.Read
                employeeId = _reader.Item("Id")
                employeeCode = _reader.Item("EmployeeCode").ToString.Trim
                employeeName = _reader.Item("EmployeeName").ToString.Trim
                positionId = _reader.Item("PositionId")
                positionName = _reader.Item("PositionName").ToString.Trim
                departmentId = _reader.Item("DepartmentId")
                departmentName = _reader.Item("DepartmentName").ToString.Trim
                employmentTypeId = _reader.Item("EmploymentTypeId")
                employmentTypeName = _reader.Item("EmploymentTypeName").ToString.Trim

                If Not _reader.Item("EmailAddress") Is DBNull.Value Then
                    emailAddress = _reader.Item("EmailAddress")
                Else
                    emailAddress = String.Empty
                End If

                If Not _reader.Item("MobileNo") Is DBNull.Value Then
                    mobileNumber = _reader.Item("MobileNo")
                Else
                    mobileNumber = 0
                End If

                If Not _reader.Item("NbcEmailAddress") Is DBNull.Value Then
                    nbcEmailAddress = _reader.Item("NbcEmailAddress")
                Else
                    nbcEmailAddress = String.Empty
                End If

                If _reader.Item("TeamId") Is DBNull.Value Then
                    teamId = 0
                    teamName = String.Empty
                Else
                    teamId = _reader.Item("TeamId")
                    teamName = _reader.Item("TeamName").ToString.Trim
                End If
            End While
            _reader.Close()

            Me.Hide()
            Dim _frmMain As New frmMain(employeeId, employeeCode, employeeName, positionId, positionName, departmentId, departmentName, teamId, teamName, employmentTypeId, employmentTypeName, emailAddress, mobileNumber, nbcEmailAddress)
            _frmMain.Show()
            txtEmployeeId.Clear()
        Else
            MessageBox.Show("Employee not found.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtEmployeeId.Clear()
            txtEmployeeId.Focus()
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Application.Exit()
    End Sub

End Class