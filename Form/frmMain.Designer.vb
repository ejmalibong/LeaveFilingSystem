<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.mnuMain = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LeaveFilingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HrListToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.PasswordToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FileToolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.LogOutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MaintenanceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EmployeeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HolidayToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.WindowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DatetimeToolStripMenuItem = New System.Windows.Forms.ToolStripLabel()
        Me.UserItemToolStripMenuItem = New System.Windows.Forms.ToolStripLabel()
        Me.UsernameToolStripMenuItem = New System.Windows.Forms.ToolStripLabel()
        Me.stsMain = New System.Windows.Forms.StatusStrip()
        Me.DepartmentToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.SectionToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.StatusToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tmrMain = New System.Windows.Forms.Timer(Me.components)
        Me.mnuMain.SuspendLayout()
        Me.stsMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'mnuMain
        '
        Me.mnuMain.BackColor = System.Drawing.Color.White
        Me.mnuMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.MaintenanceToolStripMenuItem, Me.WindowToolStripMenuItem, Me.DatetimeToolStripMenuItem, Me.UserItemToolStripMenuItem, Me.UsernameToolStripMenuItem})
        Me.mnuMain.Location = New System.Drawing.Point(0, 0)
        Me.mnuMain.MdiWindowListItem = Me.WindowToolStripMenuItem
        Me.mnuMain.Name = "mnuMain"
        Me.mnuMain.Size = New System.Drawing.Size(805, 24)
        Me.mnuMain.TabIndex = 1
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LeaveFilingToolStripMenuItem, Me.HrListToolStripMenuItem, Me.ToolStripSeparator1, Me.PasswordToolStripMenuItem, Me.FileToolStripSeparator, Me.LogOutToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'LeaveFilingToolStripMenuItem
        '
        Me.LeaveFilingToolStripMenuItem.Name = "LeaveFilingToolStripMenuItem"
        Me.LeaveFilingToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.LeaveFilingToolStripMenuItem.Text = "Leave Records"
        '
        'HrListToolStripMenuItem
        '
        Me.HrListToolStripMenuItem.Name = "HrListToolStripMenuItem"
        Me.HrListToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.HrListToolStripMenuItem.Text = "HR Records"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(165, 6)
        '
        'PasswordToolStripMenuItem
        '
        Me.PasswordToolStripMenuItem.Name = "PasswordToolStripMenuItem"
        Me.PasswordToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.PasswordToolStripMenuItem.Text = "Change Password"
        '
        'FileToolStripSeparator
        '
        Me.FileToolStripSeparator.Name = "FileToolStripSeparator"
        Me.FileToolStripSeparator.Size = New System.Drawing.Size(165, 6)
        '
        'LogOutToolStripMenuItem
        '
        Me.LogOutToolStripMenuItem.Name = "LogOutToolStripMenuItem"
        Me.LogOutToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.LogOutToolStripMenuItem.Text = "Log Out"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'MaintenanceToolStripMenuItem
        '
        Me.MaintenanceToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EmployeeToolStripMenuItem, Me.HolidayToolStripMenuItem})
        Me.MaintenanceToolStripMenuItem.Name = "MaintenanceToolStripMenuItem"
        Me.MaintenanceToolStripMenuItem.Size = New System.Drawing.Size(88, 20)
        Me.MaintenanceToolStripMenuItem.Text = "Maintenance"
        '
        'EmployeeToolStripMenuItem
        '
        Me.EmployeeToolStripMenuItem.Name = "EmployeeToolStripMenuItem"
        Me.EmployeeToolStripMenuItem.Size = New System.Drawing.Size(131, 22)
        Me.EmployeeToolStripMenuItem.Text = "Employees"
        '
        'HolidayToolStripMenuItem
        '
        Me.HolidayToolStripMenuItem.Name = "HolidayToolStripMenuItem"
        Me.HolidayToolStripMenuItem.Size = New System.Drawing.Size(131, 22)
        Me.HolidayToolStripMenuItem.Text = "Holidays"
        '
        'WindowToolStripMenuItem
        '
        Me.WindowToolStripMenuItem.Name = "WindowToolStripMenuItem"
        Me.WindowToolStripMenuItem.Size = New System.Drawing.Size(63, 20)
        Me.WindowToolStripMenuItem.Text = "Window"
        '
        'DatetimeToolStripMenuItem
        '
        Me.DatetimeToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.DatetimeToolStripMenuItem.Margin = New System.Windows.Forms.Padding(0, 1, 5, 2)
        Me.DatetimeToolStripMenuItem.Name = "DatetimeToolStripMenuItem"
        Me.DatetimeToolStripMenuItem.Padding = New System.Windows.Forms.Padding(0, 0, 10, 0)
        Me.DatetimeToolStripMenuItem.Size = New System.Drawing.Size(65, 17)
        Me.DatetimeToolStripMenuItem.Text = "Datetime"
        '
        'UserItemToolStripMenuItem
        '
        Me.UserItemToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.UserItemToolStripMenuItem.Margin = New System.Windows.Forms.Padding(0, 1, 10, 2)
        Me.UserItemToolStripMenuItem.Name = "UserItemToolStripMenuItem"
        Me.UserItemToolStripMenuItem.Size = New System.Drawing.Size(54, 17)
        Me.UserItemToolStripMenuItem.Text = "UserItem"
        '
        'UsernameToolStripMenuItem
        '
        Me.UsernameToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.UsernameToolStripMenuItem.Image = Global.LeaveFilingSystem.My.Resources.Resources.User
        Me.UsernameToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.UsernameToolStripMenuItem.Margin = New System.Windows.Forms.Padding(0, 1, 10, 2)
        Me.UsernameToolStripMenuItem.Name = "UsernameToolStripMenuItem"
        Me.UsernameToolStripMenuItem.Size = New System.Drawing.Size(76, 17)
        Me.UsernameToolStripMenuItem.Text = "Username"
        '
        'stsMain
        '
        Me.stsMain.BackColor = System.Drawing.Color.White
        Me.stsMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DepartmentToolStripStatusLabel, Me.SectionToolStripStatusLabel, Me.StatusToolStripStatusLabel})
        Me.stsMain.Location = New System.Drawing.Point(0, 239)
        Me.stsMain.Name = "stsMain"
        Me.stsMain.Size = New System.Drawing.Size(805, 22)
        Me.stsMain.SizingGrip = False
        Me.stsMain.TabIndex = 2
        '
        'DepartmentToolStripStatusLabel
        '
        Me.DepartmentToolStripStatusLabel.Name = "DepartmentToolStripStatusLabel"
        Me.DepartmentToolStripStatusLabel.Size = New System.Drawing.Size(70, 17)
        Me.DepartmentToolStripStatusLabel.Text = "Department"
        '
        'SectionToolStripStatusLabel
        '
        Me.SectionToolStripStatusLabel.Name = "SectionToolStripStatusLabel"
        Me.SectionToolStripStatusLabel.Size = New System.Drawing.Size(46, 17)
        Me.SectionToolStripStatusLabel.Text = "Section"
        '
        'StatusToolStripStatusLabel
        '
        Me.StatusToolStripStatusLabel.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.StatusToolStripStatusLabel.ForeColor = System.Drawing.Color.Red
        Me.StatusToolStripStatusLabel.Name = "StatusToolStripStatusLabel"
        Me.StatusToolStripStatusLabel.Size = New System.Drawing.Size(674, 17)
        Me.StatusToolStripStatusLabel.Spring = True
        Me.StatusToolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tmrMain
        '
        '
        'frmMain
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(805, 261)
        Me.Controls.Add(Me.stsMain)
        Me.Controls.Add(Me.mnuMain)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.MainMenuStrip = Me.mnuMain
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Leave Application"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.mnuMain.ResumeLayout(False)
        Me.mnuMain.PerformLayout
        Me.stsMain.ResumeLayout(false)
        Me.stsMain.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents mnuMain As System.Windows.Forms.MenuStrip
    Friend WithEvents stsMain As System.Windows.Forms.StatusStrip
    Friend WithEvents tmrMain As System.Windows.Forms.Timer
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents WindowToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UsernameToolStripMenuItem As System.Windows.Forms.ToolStripLabel
    Friend WithEvents UserItemToolStripMenuItem As System.Windows.Forms.ToolStripLabel
    Friend WithEvents DatetimeToolStripMenuItem As System.Windows.Forms.ToolStripLabel
    Friend WithEvents FileToolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents LogOutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LeaveFilingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DepartmentToolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents SectionToolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents HrListToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents StatusToolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents PasswordToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MaintenanceToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EmployeeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HolidayToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
