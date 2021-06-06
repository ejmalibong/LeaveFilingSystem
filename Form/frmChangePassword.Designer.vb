<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChangePassword
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
        Me.lblCurrentPassword = New System.Windows.Forms.Label()
        Me.txtCurrentPassword = New System.Windows.Forms.TextBox()
        Me.txtNewPassword = New System.Windows.Forms.TextBox()
        Me.lblNewPassword = New System.Windows.Forms.Label()
        Me.txtConfirmPassword = New System.Windows.Forms.TextBox()
        Me.lblConfirmPassword = New System.Windows.Forms.Label()
        Me.btnSave = New PinkieControls.ButtonXP()
        Me.btnClose = New PinkieControls.ButtonXP()
        Me.chkBoxShow = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'lblCurrentPassword
        '
        Me.lblCurrentPassword.BackColor = System.Drawing.SystemColors.Control
        Me.lblCurrentPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCurrentPassword.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.lblCurrentPassword.ForeColor = System.Drawing.Color.Black
        Me.lblCurrentPassword.Location = New System.Drawing.Point(12, 9)
        Me.lblCurrentPassword.Name = "lblCurrentPassword"
        Me.lblCurrentPassword.Size = New System.Drawing.Size(181, 25)
        Me.lblCurrentPassword.TabIndex = 510
        Me.lblCurrentPassword.Text = " Current Password"
        Me.lblCurrentPassword.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCurrentPassword
        '
        Me.txtCurrentPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCurrentPassword.Font = New System.Drawing.Font("Verdana", 11.0!)
        Me.txtCurrentPassword.Location = New System.Drawing.Point(192, 9)
        Me.txtCurrentPassword.Name = "txtCurrentPassword"
        Me.txtCurrentPassword.ReadOnly = True
        Me.txtCurrentPassword.Size = New System.Drawing.Size(300, 25)
        Me.txtCurrentPassword.TabIndex = 529
        '
        'txtNewPassword
        '
        Me.txtNewPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNewPassword.Font = New System.Drawing.Font("Verdana", 11.0!)
        Me.txtNewPassword.Location = New System.Drawing.Point(192, 36)
        Me.txtNewPassword.Name = "txtNewPassword"
        Me.txtNewPassword.Size = New System.Drawing.Size(300, 25)
        Me.txtNewPassword.TabIndex = 531
        '
        'lblNewPassword
        '
        Me.lblNewPassword.BackColor = System.Drawing.SystemColors.Control
        Me.lblNewPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNewPassword.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.lblNewPassword.ForeColor = System.Drawing.Color.Black
        Me.lblNewPassword.Location = New System.Drawing.Point(12, 36)
        Me.lblNewPassword.Name = "lblNewPassword"
        Me.lblNewPassword.Size = New System.Drawing.Size(181, 25)
        Me.lblNewPassword.TabIndex = 530
        Me.lblNewPassword.Text = " New Password"
        Me.lblNewPassword.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtConfirmPassword
        '
        Me.txtConfirmPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtConfirmPassword.Font = New System.Drawing.Font("Verdana", 11.0!)
        Me.txtConfirmPassword.Location = New System.Drawing.Point(192, 63)
        Me.txtConfirmPassword.Name = "txtConfirmPassword"
        Me.txtConfirmPassword.Size = New System.Drawing.Size(300, 25)
        Me.txtConfirmPassword.TabIndex = 533
        '
        'lblConfirmPassword
        '
        Me.lblConfirmPassword.BackColor = System.Drawing.SystemColors.Control
        Me.lblConfirmPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblConfirmPassword.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.lblConfirmPassword.ForeColor = System.Drawing.Color.Black
        Me.lblConfirmPassword.Location = New System.Drawing.Point(12, 63)
        Me.lblConfirmPassword.Name = "lblConfirmPassword"
        Me.lblConfirmPassword.Size = New System.Drawing.Size(181, 25)
        Me.lblConfirmPassword.TabIndex = 532
        Me.lblConfirmPassword.Text = " Confirm New Password"
        Me.lblConfirmPassword.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnSave.DefaultScheme = False
        Me.btnSave.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnSave.Font = New System.Drawing.Font("Verdana", 10.5!)
        Me.btnSave.Hint = "Save changes"
        Me.btnSave.Image = Global.LeaveFilingSystem.My.Resources.Resources.Save_16_x_16
        Me.btnSave.Location = New System.Drawing.Point(286, 103)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Scheme = PinkieControls.ButtonXP.Schemes.Blue
        Me.btnSave.Size = New System.Drawing.Size(100, 36)
        Me.btnSave.TabIndex = 534
        Me.btnSave.TabStop = False
        Me.btnSave.Text = " Save"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnClose.DefaultScheme = False
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Verdana", 10.5!)
        Me.btnClose.Hint = "Close form"
        Me.btnClose.Location = New System.Drawing.Point(392, 103)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Scheme = PinkieControls.ButtonXP.Schemes.Blue
        Me.btnClose.Size = New System.Drawing.Size(100, 36)
        Me.btnClose.TabIndex = 535
        Me.btnClose.TabStop = False
        Me.btnClose.Text = "Close"
        '
        'chkBoxShow
        '
        Me.chkBoxShow.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkBoxShow.AutoSize = True
        Me.chkBoxShow.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.chkBoxShow.Location = New System.Drawing.Point(12, 110)
        Me.chkBoxShow.Name = "chkBoxShow"
        Me.chkBoxShow.Size = New System.Drawing.Size(138, 21)
        Me.chkBoxShow.TabIndex = 537
        Me.chkBoxShow.Text = "Show Password"
        Me.chkBoxShow.UseVisualStyleBackColor = True
        '
        'frmChangePassword
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(504, 151)
        Me.Controls.Add(Me.chkBoxShow)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.txtConfirmPassword)
        Me.Controls.Add(Me.lblConfirmPassword)
        Me.Controls.Add(Me.txtNewPassword)
        Me.Controls.Add(Me.lblNewPassword)
        Me.Controls.Add(Me.txtCurrentPassword)
        Me.Controls.Add(Me.lblCurrentPassword)
        Me.Font = New System.Drawing.Font("Verdana", 8.5!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmChangePassword"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Change Password"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblCurrentPassword As System.Windows.Forms.Label
    Friend WithEvents txtCurrentPassword As System.Windows.Forms.TextBox
    Friend WithEvents txtNewPassword As System.Windows.Forms.TextBox
    Friend WithEvents lblNewPassword As System.Windows.Forms.Label
    Friend WithEvents txtConfirmPassword As System.Windows.Forms.TextBox
    Friend WithEvents lblConfirmPassword As System.Windows.Forms.Label
    Friend WithEvents btnSave As PinkieControls.ButtonXP
    Friend WithEvents btnClose As PinkieControls.ButtonXP
    Friend WithEvents chkBoxShow As System.Windows.Forms.CheckBox
End Class
