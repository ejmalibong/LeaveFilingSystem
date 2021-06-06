Imports System.Data.SqlClient
Imports System.ComponentModel
Imports BlackCoffeeLibrary.BlackCoffee
Imports LeaveFilingSystem
Imports LeaveFilingSystem.dsLeaveFiling
Imports LeaveFilingSystem.dsLeaveFilingTableAdapters
Imports System.Net.Mail

Public Class frmMain
    Private connection As New clsConnection
    Private dbLeaveFiling As New SqlDbMethod(connection.LocalConnection)
    Private dbJeonsoft As New SqlDbMethod(connection.JeonsoftConnection)
    Private main As New Main
    'server datetime
    Private serverDate As DateTime = dbLeaveFiling.GetServerDate
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
    'check email if already sent
    Private Shared mailSent As Boolean = False
    'email sender
    Private systemEmailAddress As String = String.Empty
    Private systemEmailPassword As String = String.Empty
    'for debugging purposes
    Private isDebug As Boolean = False

    Public Sub New(ByVal _employeeId As Integer, ByVal _employeeCode As String, ByVal _employeeName As String, _
                   ByVal _positionId As Integer, ByVal _positionName As String, ByVal _departmentId As Integer, _
                   ByVal _departmentName As String, ByVal _teamId As Integer, ByVal _teamName As String, _
                   ByVal _employmentTypeId As Integer, ByVal _employmentTypeName As String, ByVal _emailAddress As String, _
                   ByVal _mobileNumber As String, ByVal _nbcEmailAddress As String)

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

        ControlAccess(employeeId)

        If teamId = 1 Then
            If isHrRecords(employeeId) = True Then
                main.FormLoader(Me, New frmHrRecord(employeeId, positionId, departmentId, teamId, employmentTypeId), True)
            Else
                main.FormLoader(Me, New frmLeaveRecord(employeeId, positionId, departmentId, teamId, employmentTypeId), True)
            End If
        Else
            main.FormLoader(Me, New frmLeaveRecord(employeeId, positionId, departmentId, teamId, employmentTypeId), True)
        End If

        GetEmailSettings(1)
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

    Private Sub frmMain_MdiChildActivate(sender As Object, e As EventArgs) Handles MyBase.MdiChildActivate
        Dim _frm As Form = Me.ActiveMdiChild

        If Not _frm Is Nothing Then
            Me.Text = "Leave Application - " & _frm.Text & ""
        Else
            Me.Text = "Leave Application"
        End If
    End Sub

    Private Sub tmrMain_Tick(sender As Object, e As EventArgs) Handles tmrMain.Tick
        DatetimeToolStripMenuItem.Text = serverDate.ToString("MMMM dd, yyyy")
    End Sub

    'file
    Private Sub LeaveFilingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LeaveFilingToolStripMenuItem.Click
        main.FormLoader(Me, New frmLeaveRecord(employeeId, positionId, departmentId, teamId, employmentTypeId), True)
    End Sub

    Private Sub HrListToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HrListToolStripMenuItem.Click
        main.FormLoader(Me, New frmHrRecord(employeeId, positionId, departmentId, teamId, employmentTypeId), True)
    End Sub

    Private Sub PasswordToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PasswordToolStripMenuItem.Click
        main.FormLoader(Me, New frmChangePassword(employeeId))
    End Sub

    Private Sub LogOutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LogOutToolStripMenuItem.Click
        Me.Hide()

        If Environment.MachineName.Substring(0, 8) = "NBCP-MDT" Then
            frmLoginScan.Show()
            frmLoginScan.txtEmployeeId.Clear()
        Else
            frmLogin.Show()
            frmLogin.txtEmployeeId.Clear()
            frmLogin.txtPassword.Clear()
        End If

        'frmLoginScan.Show()
        'frmLoginScan.txtEmployeeId.Clear()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Application.Exit()
    End Sub

    'maintenance
    Private Sub EmployeeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EmployeeToolStripMenuItem.Click
        main.FormLoader(Me, New frmEmployee, True)
    End Sub

    Private Sub HolidayToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HolidayToolStripMenuItem.Click
        main.FormLoader(Me, New frmHoliday, False)
    End Sub

#Region "Sub"
    'check if hr to enable hr records, employee and holidays
    Private Function IsHrRecords(ByVal _employeeId As Integer) As Boolean
        Dim _isAllow As Boolean = False

        Try
            Dim _prmEmpId(0) As SqlParameter
            _prmEmpId(0) = New SqlParameter("@EmployeeId", SqlDbType.Int)
            _prmEmpId(0).Value = _employeeId

            _isAllow = dbLeaveFiling.ExecuteScalar("SELECT IsHrRecords FROM dbo.Employee WHERE EmployeeId = @EmployeeId", _
                                                   CommandType.Text, _prmEmpId)
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        If _isAllow = True Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub ControlAccess(ByVal _employeeId As Integer)
        Try
            Dim _prmEmpId(0) As SqlParameter
            _prmEmpId(0) = New SqlParameter("@EmployeeId", SqlDbType.Int)
            _prmEmpId(0).Value = _employeeId

            Dim _reader As IDataReader = dbLeaveFiling.ExecuteReader("SELECT IsHrRecords, IsEmployee, IsHoliday FROM dbo.Employee WHERE EmployeeId = @EmployeeId", _
                                        CommandType.Text, _prmEmpId)

            While _reader.Read
                HrListToolStripMenuItem.Visible = _reader.Item("IsHrRecords")
                EmployeeToolStripMenuItem.Visible = _reader.Item("IsEmployee")
                HolidayToolStripMenuItem.Visible = _reader.Item("IsHoliday")
            End While
            _reader.Close()


        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'get email settings to be use for sending email notifications
    Private Sub GetEmailSettings(ByVal _settingsId As Integer)
        Try
            Dim _prmSettingsId(0) As SqlParameter
            _prmSettingsId(0) = New SqlParameter("@SettingsId", SqlDbType.Int)
            _prmSettingsId(0).Value = _settingsId

            Dim _reader As IDataReader = dbLeaveFiling.ExecuteReader("SELECT TRIM(EmailAddress) AS EmailAddress, TRIM(EmailPassword) AS EmailPassword " & _
                                                                     "FROM dbo.Settings WHERE SettingsId = @SettingsId", _
                                                                     CommandType.Text, _prmSettingsId)

            While _reader.Read
                systemEmailAddress = _reader.Item("EmailAddress").ToString.Trim
                systemEmailPassword = _reader.Item("EmailPassword").ToString.Trim
            End While
            _reader.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

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

    'send remarks from superiors or manager
    Public Sub SendRemarksNofitication(ByVal _employeeId As Integer, ByVal _remarksPosition As String, _
                                       ByVal _remarksFrom As String, ByVal _remarks As String, _
                                       ByVal _leaveType As String, ByVal _startDate As Date, _
                                       ByVal _endDate As Date)
        Try
            Dim client As New SmtpClient()
            Dim message As New MailMessage()
            Dim messageBody As String = "<font size=""3"" face=""Segoe UI"" color=""black"">" & _
                                        "Good day! <br> <br> "

            message.From = New MailAddress(systemEmailAddress, "NBC Leave Notication")

            Dim _prmRequestor(0) As SqlParameter
            _prmRequestor(0) = New SqlParameter("@EmployeeId", SqlDbType.Int)
            _prmRequestor(0).Value = _employeeId

            Dim _reader As IDataReader = dbJeonsoft.ExecuteReader("SELECT TRIM(EmailAddress) AS EmailAddress FROM dbo.tblEmployees WHERE Id = @EmployeeId", _
                                                                  CommandType.Text, _prmRequestor)

            While _reader.Read
                If _reader.Item("EmailAddress") Is DBNull.Value Then
                    message.To.Add("it1@nbc-p.com")
                Else
                    If isDebug = True Then
                        message.To.Add("it1@nbc-p.com")
                    Else
                        message.To.Add(_reader.Item("EmailAddress").ToString.Trim)
                    End If
                End If
            End While
            _reader.Close()

            message.Subject = "Leave Notification"
            message.IsBodyHtml = True 'set email as html to attach hyperlink

            messageBody += "New notification to your " & _leaveType.ToString.Trim

            If _startDate.Date.Date.Equals(_endDate.Date.Date) Then
                messageBody += " dated " & _startDate.Date.ToString("MMMM dd, yyyy")
            Else
                messageBody += " dated " & _startDate.Date.ToString("MMMM dd, yyyy") & _
                               " to " & _endDate.Date.ToString("MMMM dd, yyyy")
            End If

            messageBody += " from your " & _remarksPosition.ToString.Trim & ". <br> <br>" & _
                            StrConv(_remarksFrom.ToString.Trim, VbStrConv.ProperCase) & " : " & _remarks.ToString.Trim

            messageBody += "<br> <br> <br>" & _
                           "Please check on your Leave Application System." & _
                           "<br> <br>" & _
                           "If you have any concerns, please call IT (Local 232). Thank you. <br> <br>" & _
                           "This is a system-generated email. Please do not reply."

            message.Body = messageBody

            client.Host = "smtp.gmail.com"
            client.Port = 587
            client.EnableSsl = True
            client.UseDefaultCredentials = False
            client.Credentials = New Net.NetworkCredential(systemEmailAddress, systemEmailPassword)

            Dim userState As String = "userState"
            AddHandler client.SendCompleted, AddressOf SendCompletedCallback

            client.SendAsync(message, userState)

            StatusToolStripStatusLabel.Visible = True
            StatusToolStripStatusLabel.Text = "Sending email. Please wait......"
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'returned for revision
    Public Sub SendEmailReturned(ByVal _employeeId As Integer, ByVal _leaveType As String, ByVal _startDate As Date, ByVal _endDate As Date)
        Try
            Dim client As New SmtpClient()
            Dim message As New MailMessage()
            Dim messageBody As String = "<font size=""3"" face=""Segoe UI"" color=""black"">" & _
                                        "Good day! <br> <br> "

            message.From = New MailAddress(systemEmailAddress, "NBC Leave Notication")

            Dim _prmRequestor(0) As SqlParameter
            _prmRequestor(0) = New SqlParameter("@EmployeeId", SqlDbType.Int)
            _prmRequestor(0).Value = _employeeId

            Dim _reader As IDataReader = dbJeonsoft.ExecuteReader("SELECT TRIM(EmailAddress) AS EmailAddress FROM dbo.tblEmployees WHERE Id = @EmployeeId", _
                                                                  CommandType.Text, _prmRequestor)

            While _reader.Read
                If _reader.Item("EmailAddress") Is DBNull.Value Then
                    message.To.Add("it1@nbc-p.com")
                Else
                    If isDebug = True Then
                        message.To.Add("it1@nbc-p.com")
                    Else
                        message.To.Add(_reader.Item("EmailAddress").ToString.Trim)
                    End If
                End If
            End While
            _reader.Close()

            message.Subject = "Leave Notification"
            message.IsBodyHtml = True 'set email as html to attach hyperlink

            messageBody += "Your " & _leaveType.ToString.Trim

            If _startDate.Date.Date.Equals(_endDate.Date.Date) Then
                messageBody += " dated " & _startDate.Date.ToString("MMMM dd, yyyy")
            Else
                messageBody += " dated from " & _startDate.Date.ToString("MMMM dd, yyyy") & " to " & _endDate.Date.ToString("MMMM dd, yyyy")
            End If

            messageBody += " is returned for revision. Please update your application then resend it to your approvers. <br> <br>" & _
                           "If you have any concerns, please call IT (Local 232). Thank you. <br> <br>" & _
                           "This is a system-generated email. Please do not reply."

            message.Body = messageBody

            client.Host = "smtp.gmail.com"
            client.Port = 587
            client.EnableSsl = True
            client.UseDefaultCredentials = False
            client.Credentials = New Net.NetworkCredential(systemEmailAddress, systemEmailPassword)

            Dim userState As String = "userState"
            AddHandler client.SendCompleted, AddressOf SendCompletedCallback

            client.SendAsync(message, userState)

            StatusToolStripStatusLabel.Visible = True
            StatusToolStripStatusLabel.Text = "Sending email. Please wait......"
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'send email to requestor
    Public Sub SendEmailRequestor(ByVal _isApproved As Boolean, ByVal _employeeId As Integer, ByVal _leaveType As String, ByVal _startDate As Date, ByVal _endDate As Date)
        Try
            Dim client As New SmtpClient()
            Dim message As New MailMessage()
            Dim messageBody As String = "<font size=""3"" face=""Segoe UI"" color=""black"">" & _
                                        "Good day! <br> <br> "

            message.From = New MailAddress(systemEmailAddress, "NBC Leave Notification")

            Dim _prmRequestor(0) As SqlParameter
            _prmRequestor(0) = New SqlParameter("@EmployeeId", SqlDbType.Int)
            _prmRequestor(0).Value = _employeeId

            Dim _reader As IDataReader = dbJeonsoft.ExecuteReader("SELECT TRIM(EmailAddress) AS EmailAddress FROM dbo.tblEmployees WHERE Id = @EmployeeId", _
                                                                  CommandType.Text, _prmRequestor)

            While _reader.Read
                If _reader.Item("EmailAddress") Is DBNull.Value Then
                    message.To.Add("it1@nbc-p.com")
                Else
                    If isDebug = True Then
                        message.To.Add("it1@nbc-p.com")
                    Else
                        message.To.Add(_reader.Item("EmailAddress").ToString.Trim)
                    End If
                End If
            End While
            _reader.Close()

            message.Subject = "Leave Notification"
            message.IsBodyHtml = True 'set email as html to attach hyperlink

            If _isApproved = True Then
                If _startDate.Date.Date.Equals(_endDate.Date.Date) Then
                    messageBody += "Your " & _leaveType.ToString.Trim & " dated " & _startDate.Date.ToString("MMMM dd, yyyy") & _
                                   " is approved. <br> <br>" & _
                                   "If you have any concerns, please call IT (Local 232). Thank you. <br> <br>" & _
                                   "This is a system-generated email. Please do not reply."
                Else
                    messageBody += "Your " & _leaveType.ToString.Trim & " dated from " & _startDate.Date.ToString("MMMM dd, yyyy") & _
                                   " to " & _endDate.Date.ToString("MMMM dd, yyyy") & " is approved. <br> <br>" & _
                                   "If you have any concerns, please call IT (Local 232). Thank you. <br> <br>" & _
                                   "This is a system-generated email. Please do not reply."
                End If

            Else
                If _startDate.Date.Date.Equals(_endDate.Date.Date) Then
                    messageBody += "Your " & _leaveType.ToString.Trim & " dated " & _startDate.Date.ToString("MMMM dd, yyyy") & _
                                   " is disapproved. <br> <br>" & _
                                   "If you have any concerns, please call IT (Local 232). Thank you. <br> <br>" & _
                                   "This is a system-generated email. Please do not reply."
                Else
                    messageBody += "Your " & _leaveType.ToString.Trim & " dated from " & _startDate.Date.ToString("MMMM dd, yyyy") & _
                                   " to " & _endDate.Date.ToString("MMMM dd, yyyy") & " is disapproved. <br> <br>" & _
                                   "If you have any concerns, please call IT (Local 232). Thank you. <br> <br>" & _
                                   "This is a system-generated email. Please do not reply."
                End If
            End If

            message.Body = messageBody

            client.Host = "smtp.gmail.com"
            client.Port = 587
            client.EnableSsl = True
            client.UseDefaultCredentials = False
            client.Credentials = New Net.NetworkCredential(systemEmailAddress, systemEmailPassword)

            Dim userState As String = "userState"
            AddHandler client.SendCompleted, AddressOf SendCompletedCallback

            client.SendAsync(message, userState)

            StatusToolStripStatusLabel.Visible = True
            StatusToolStripStatusLabel.Text = "Sending email. Please wait......"
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'send email to next approver
    Public Sub SendEmailApprovers(ByVal _leaveFileId As Integer, ByVal _employeeId As Integer, ByVal _leaveType As String, ByVal _employeeName As String, _
                                  ByVal _department As String, ByVal _date As String, ByVal _reason As String)
        Try
            Dim client As New SmtpClient()
            Dim message As New MailMessage()
            Dim messageBody As String = "<font size=""3"" face=""Segoe UI"" color=""black"">" & _
                                        "Good day! <br> <br> " & _
                                        "New leave application for your approval. Please check the information below for your reference. <br> <br> " & _
                                        "<table style=""font-size: 20px;font-family:Segoe UI""> " & _
                                        "<tr><td style=""width:10px""></td><td>Leave File ID: </td><td style=""width:50px""></td><td>" & _leaveFileId & "</td></tr>" & _
                                        "<tr><td style=""width:10px""></td><td>Leave Type: </td><td style=""width:50px""></td><td>" & _leaveType.ToString.Trim & "</td></tr>" & _
                                        "<tr><td style=""width:10px""></td><td>Employee Name: </td><td style=""width:50px""></td><td>" & _employeeName.ToString.Trim & "</td></tr>" & _
                                        "<tr><td style=""width:10px""></td><td>Department/Section: </td><td style=""width:50px""></td><td>" & _department.ToString.Trim & "</td></tr>" & _
                                        "<tr><td style=""width:10px""></td><td>Date: </td><td style=""width:50px""></td><td>" & _date.ToString.Trim & "</td></tr>" & _
                                        "<tr><td style=""width:10px""></td><td>Reason: </td><td style=""width:50px""></td><td>" & _reason.ToString.Trim & "</td></tr>" & _
                                        "</table>" & _
                                        "<br>" & _
                                        "Please check on your Leave Application System." & _
                                        "<br> <br>" & _
                                        "If you have any concerns, please call IT (Local 232). Thank you." & _
                                        "<br> <br>" & _
                                        "This is a system-generated email. Please do not reply."

            message.From = New MailAddress(systemEmailAddress, "NBC Leave Notification")

            Dim _prmApprover(0) As SqlParameter
            _prmApprover(0) = New SqlParameter("@EmployeeId", SqlDbType.Int)
            _prmApprover(0).Value = _employeeId

            Dim _reader As IDataReader = dbLeaveFiling.ExecuteReader("SELECT TRIM(NbcEmailAddress) AS NbcEmailAddress FROM dbo.Employee WHERE EmployeeId = @EmployeeId", _
                                                                     CommandType.Text, _prmApprover)

            While _reader.Read
                If _reader.Item("NbcEmailAddress") Is DBNull.Value Then
                    message.To.Add("it1@nbc-p.com")
                Else
                    If isDebug = True Then
                        message.To.Add("it1@nbc-p.com")
                    Else
                        message.To.Add(_reader.Item("NbcEmailAddress").ToString.Trim)
                    End If
                End If
            End While
            _reader.Close()

            message.Subject = "Leave Notification"
            message.IsBodyHtml = True 'set email as html to attach hyperlink
            message.Body = messageBody

            client.Host = "smtp.gmail.com"
            client.Port = 587
            client.UseDefaultCredentials = False
            client.EnableSsl = True
            client.Credentials = New Net.NetworkCredential(systemEmailAddress, systemEmailPassword)

            Dim userState As String = "userState"
            AddHandler client.SendCompleted, AddressOf SendCompletedCallback

            client.SendAsync(message, userState)

            StatusToolStripStatusLabel.Visible = True
            StatusToolStripStatusLabel.Text = "Sending email. Please wait......"
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'send email to hr (mam cath) for encoding to jeonsoft
    Public Sub SendEmailHr(ByVal _leaveFileId As Integer, ByVal _isApproved As Boolean, _leaveType As String, ByVal _employeeName As String, ByVal _department As String, ByVal _date As String, ByVal _reason As String)
        Try
            Dim client As New SmtpClient()
            Dim message As New MailMessage()
            Dim messageBody As String = String.Empty

            If _isApproved = True Then
                messageBody = "<font size=""3"" face=""Segoe UI"" color=""black"">" & _
                            "Good day! <br> <br> " & _
                            "New approved leave application. Please check the information below for your reference. <br> <br> " & _
                            "<table style=""font-size: 20px;font-family:Segoe UI""> " & _
                            "<tr><td style=""width:10px""></td><td>Leave File ID: </td><td style=""width:50px""></td><td>" & _leaveFileId & "</td></tr>" & _
                            "<tr><td style=""width:10px""></td><td>Leave Type: </td><td style=""width:50px""></td><td>" & _leaveType.ToString.Trim & "</td></tr>" & _
                            "<tr><td style=""width:10px""></td><td>Employee Name: </td><td style=""width:50px""></td><td>" & _employeeName.ToString.Trim & "</td></tr>" & _
                            "<tr><td style=""width:10px""></td><td>Department/Section: </td><td style=""width:50px""></td><td>" & _department.ToString.Trim & "</td></tr>" & _
                            "<tr><td style=""width:10px""></td><td>Date: </td><td style=""width:50px""></td><td>" & _date.ToString.Trim & "</td></tr>" & _
                            "<tr><td style=""width:10px""></td><td>Reason: </td><td style=""width:50px""></td><td>" & _reason.ToString.Trim & "</td></tr>" & _
                            "</table>" & _
                            "<br>" & _
                            "Please check on your Leave Application System." & _
                            "<br> <br>" & _
                            "If you have any concerns, please call IT (Local 232). <br> <br>" & _
                            "This is a system-generated email. Please do not reply."
            Else
                messageBody = "<font size=""3"" face=""Segoe UI"" color=""black"">" & _
                            "Good day! <br> <br> " & _
                            "New disapproved leave application. Please check the information below for your reference. <br> <br> " & _
                            "<table style=""font-size: 20px;font-family:Segoe UI""> " & _
                            "<tr><td style=""width:10px""></td><td>Leave File ID: </td><td style=""width:50px""></td><td>" & _leaveFileId & "</td></tr>" & _
                            "<tr><td style=""width:10px""></td><td>Leave Type: </td><td style=""width:50px""></td><td>" & _leaveType.ToString.Trim & "</td></tr>" & _
                            "<tr><td style=""width:10px""></td><td>Employee Name: </td><td style=""width:50px""></td><td>" & _employeeName.ToString.Trim & "</td></tr>" & _
                            "<tr><td style=""width:10px""></td><td>Department/Section: </td><td style=""width:50px""></td><td>" & _department.ToString.Trim & "</td></tr>" & _
                            "<tr><td style=""width:10px""></td><td>Date: </td><td style=""width:50px""></td><td>" & _date.ToString.Trim & "</td></tr>" & _
                            "<tr><td style=""width:10px""></td><td>Reason: </td><td style=""width:50px""></td><td>" & _reason.ToString.Trim & "</td></tr>" & _
                            "</table>" & _
                            "<br>" & _
                            "Please check on your Leave Application System." & _
                            "<br> <br>" & _
                            "If you have any concerns, please call IT (Local 232). Thank you. <br> <br>" & _
                            "This is a system-generated email. Please do not reply."
            End If

            message.From = New MailAddress(systemEmailAddress, "NBC Leave Notification")

            If isDebug = True Then
                message.To.Add("it1@nbc-p.com")
            Else
                message.To.Add("catherine.delapena@nbc-p.com")
            End If

            message.Subject = "Leave Notification"
            message.IsBodyHtml = True
            message.Body = messageBody

            client.Host = "smtp.gmail.com"
            client.Port = 587
            client.EnableSsl = True
            client.UseDefaultCredentials = False
            client.Credentials = New Net.NetworkCredential(systemEmailAddress, systemEmailPassword)

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
                StatusToolStripStatusLabel.Text = "Send canceled."
            End If

            If e.Error IsNot Nothing Then
                StatusToolStripStatusLabel.Text = e.Error.ToString
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
