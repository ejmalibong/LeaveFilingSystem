<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLoginScan
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLoginScan))
        Me.picLogo = New System.Windows.Forms.PictureBox()
        Me.lblEmployeeId = New System.Windows.Forms.Label()
        Me.btnClose = New PinkieControls.ButtonXP()
        Me.btnLogin = New PinkieControls.ButtonXP()
        Me.txtEmployeeId = New System.Windows.Forms.TextBox()
        Me.lblVersion = New System.Windows.Forms.Label()
        CType(Me.picLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'picLogo
        '
        Me.picLogo.BackColor = System.Drawing.Color.White
        Me.picLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.picLogo.Image = CType(resources.GetObject("picLogo.Image"), System.Drawing.Image)
        Me.picLogo.Location = New System.Drawing.Point(13, 14)
        Me.picLogo.Name = "picLogo"
        Me.picLogo.Size = New System.Drawing.Size(200, 170)
        Me.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picLogo.TabIndex = 96
        Me.picLogo.TabStop = False
        '
        'lblEmployeeId
        '
        Me.lblEmployeeId.AutoSize = True
        Me.lblEmployeeId.Location = New System.Drawing.Point(226, 29)
        Me.lblEmployeeId.Name = "lblEmployeeId"
        Me.lblEmployeeId.Size = New System.Drawing.Size(86, 19)
        Me.lblEmployeeId.TabIndex = 103
        Me.lblEmployeeId.Text = "Employee ID"
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnClose.DefaultScheme = True
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Hint = "Close"
        Me.btnClose.Location = New System.Drawing.Point(382, 91)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Scheme = PinkieControls.ButtonXP.Schemes.Blue
        Me.btnClose.Size = New System.Drawing.Size(110, 36)
        Me.btnClose.TabIndex = 102
        Me.btnClose.Text = "Close"
        '
        'btnLogin
        '
        Me.btnLogin.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnLogin.DefaultScheme = True
        Me.btnLogin.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnLogin.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLogin.Hint = "Login"
        Me.btnLogin.Location = New System.Drawing.Point(266, 91)
        Me.btnLogin.Name = "btnLogin"
        Me.btnLogin.Scheme = PinkieControls.ButtonXP.Schemes.Blue
        Me.btnLogin.Size = New System.Drawing.Size(110, 36)
        Me.btnLogin.TabIndex = 101
        Me.btnLogin.Text = "Login"
        '
        'txtEmployeeId
        '
        Me.txtEmployeeId.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtEmployeeId.Font = New System.Drawing.Font("Segoe UI", 14.0!)
        Me.txtEmployeeId.Location = New System.Drawing.Point(230, 52)
        Me.txtEmployeeId.Name = "txtEmployeeId"
        Me.txtEmployeeId.Size = New System.Drawing.Size(262, 32)
        Me.txtEmployeeId.TabIndex = 99
        Me.txtEmployeeId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblVersion
        '
        Me.lblVersion.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblVersion.AutoSize = True
        Me.lblVersion.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVersion.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblVersion.Location = New System.Drawing.Point(451, 191)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(45, 15)
        Me.lblVersion.TabIndex = 105
        Me.lblVersion.Text = "version"
        '
        'frmLoginScan
        '
        Me.AcceptButton = Me.btnLogin
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(504, 211)
        Me.Controls.Add(Me.lblVersion)
        Me.Controls.Add(Me.lblEmployeeId)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnLogin)
        Me.Controls.Add(Me.txtEmployeeId)
        Me.Controls.Add(Me.picLogo)
        Me.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmLoginScan"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Leave Application"
        CType(Me.picLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents picLogo As System.Windows.Forms.PictureBox
    Friend WithEvents lblEmployeeId As System.Windows.Forms.Label
    Friend WithEvents btnClose As PinkieControls.ButtonXP
    Friend WithEvents btnLogin As PinkieControls.ButtonXP
    Friend WithEvents txtEmployeeId As System.Windows.Forms.TextBox
    Friend WithEvents lblVersion As System.Windows.Forms.Label
End Class
