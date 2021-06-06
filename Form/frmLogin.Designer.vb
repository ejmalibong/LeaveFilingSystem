<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLogin
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLogin))
        Me.txtEmployeeId = New System.Windows.Forms.TextBox()
        Me.btnLogin = New PinkieControls.ButtonXP()
        Me.btnClose = New PinkieControls.ButtonXP()
        Me.picLogo = New System.Windows.Forms.PictureBox()
        Me.lblVersion = New System.Windows.Forms.Label()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.lblPassword = New System.Windows.Forms.Label()
        Me.lblEmployeeId = New System.Windows.Forms.Label()
        Me.chkBoxShow = New System.Windows.Forms.CheckBox()
        CType(Me.picLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtEmployeeId
        '
        Me.txtEmployeeId.Font = New System.Drawing.Font("Segoe UI", 14.0!)
        Me.txtEmployeeId.Location = New System.Drawing.Point(230, 37)
        Me.txtEmployeeId.Name = "txtEmployeeId"
        Me.txtEmployeeId.Size = New System.Drawing.Size(262, 32)
        Me.txtEmployeeId.TabIndex = 0
        Me.txtEmployeeId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnLogin
        '
        Me.btnLogin.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLogin.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnLogin.DefaultScheme = True
        Me.btnLogin.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnLogin.Font = New System.Drawing.Font("Verdana", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLogin.Hint = "Login"
        Me.btnLogin.Location = New System.Drawing.Point(266, 178)
        Me.btnLogin.Name = "btnLogin"
        Me.btnLogin.Scheme = PinkieControls.ButtonXP.Schemes.Blue
        Me.btnLogin.Size = New System.Drawing.Size(110, 36)
        Me.btnLogin.TabIndex = 2
        Me.btnLogin.Text = "Login"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnClose.DefaultScheme = True
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Verdana", 10.5!)
        Me.btnClose.Hint = "Close"
        Me.btnClose.Location = New System.Drawing.Point(382, 178)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Scheme = PinkieControls.ButtonXP.Schemes.Blue
        Me.btnClose.Size = New System.Drawing.Size(110, 36)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "Close"
        '
        'picLogo
        '
        Me.picLogo.BackColor = System.Drawing.Color.White
        Me.picLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.picLogo.Image = CType(resources.GetObject("picLogo.Image"), System.Drawing.Image)
        Me.picLogo.Location = New System.Drawing.Point(13, 28)
        Me.picLogo.Name = "picLogo"
        Me.picLogo.Size = New System.Drawing.Size(200, 170)
        Me.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picLogo.TabIndex = 95
        Me.picLogo.TabStop = False
        '
        'lblVersion
        '
        Me.lblVersion.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblVersion.AutoSize = True
        Me.lblVersion.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVersion.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblVersion.Location = New System.Drawing.Point(451, 221)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(45, 15)
        Me.lblVersion.TabIndex = 97
        Me.lblVersion.Text = "version"
        '
        'txtPassword
        '
        Me.txtPassword.Font = New System.Drawing.Font("Segoe UI", 14.0!)
        Me.txtPassword.Location = New System.Drawing.Point(230, 95)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(9679)
        Me.txtPassword.Size = New System.Drawing.Size(262, 32)
        Me.txtPassword.TabIndex = 1
        Me.txtPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtPassword.UseSystemPasswordChar = True
        '
        'lblPassword
        '
        Me.lblPassword.AutoSize = True
        Me.lblPassword.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.lblPassword.Location = New System.Drawing.Point(226, 72)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(70, 20)
        Me.lblPassword.TabIndex = 100
        Me.lblPassword.Text = "Password"
        '
        'lblEmployeeId
        '
        Me.lblEmployeeId.AutoSize = True
        Me.lblEmployeeId.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.lblEmployeeId.Location = New System.Drawing.Point(226, 14)
        Me.lblEmployeeId.Name = "lblEmployeeId"
        Me.lblEmployeeId.Size = New System.Drawing.Size(94, 20)
        Me.lblEmployeeId.TabIndex = 99
        Me.lblEmployeeId.Text = "Employee ID"
        '
        'chkBoxShow
        '
        Me.chkBoxShow.AutoSize = True
        Me.chkBoxShow.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.chkBoxShow.Location = New System.Drawing.Point(230, 132)
        Me.chkBoxShow.Name = "chkBoxShow"
        Me.chkBoxShow.Size = New System.Drawing.Size(138, 21)
        Me.chkBoxShow.TabIndex = 538
        Me.chkBoxShow.Text = "Show Password"
        Me.chkBoxShow.UseVisualStyleBackColor = True
        '
        'frmLogin
        '
        Me.AcceptButton = Me.btnLogin
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(504, 241)
        Me.Controls.Add(Me.chkBoxShow)
        Me.Controls.Add(Me.lblPassword)
        Me.Controls.Add(Me.lblEmployeeId)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.lblVersion)
        Me.Controls.Add(Me.picLogo)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnLogin)
        Me.Controls.Add(Me.txtEmployeeId)
        Me.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmLogin"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Leave Application"
        CType(Me.picLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtEmployeeId As System.Windows.Forms.TextBox
    Friend WithEvents btnLogin As PinkieControls.ButtonXP
    Friend WithEvents btnClose As PinkieControls.ButtonXP
    Friend WithEvents picLogo As System.Windows.Forms.PictureBox
    Friend WithEvents lblVersion As System.Windows.Forms.Label
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents lblPassword As System.Windows.Forms.Label
    Friend WithEvents lblEmployeeId As System.Windows.Forms.Label
    Friend WithEvents chkBoxShow As System.Windows.Forms.CheckBox
End Class
