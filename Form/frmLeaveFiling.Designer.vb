<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLeaveFiling
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
        Me.txtTotalLeaveCredits = New System.Windows.Forms.Label()
        Me.lblTotalLeaveCredits = New System.Windows.Forms.Label()
        Me.lblFrom = New System.Windows.Forms.Label()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.lblTo = New System.Windows.Forms.Label()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.lblDatesOfLeave = New System.Windows.Forms.Label()
        Me.lblNumberOfDays = New System.Windows.Forms.Label()
        Me.txtNumberOfDays = New System.Windows.Forms.Label()
        Me.lblReason = New System.Windows.Forms.Label()
        Me.txtReason = New System.Windows.Forms.TextBox()
        Me.btnClose = New PinkieControls.ButtonXP()
        Me.btnDelete = New PinkieControls.ButtonXP()
        Me.btnCancel = New PinkieControls.ButtonXP()
        Me.txtBalance = New System.Windows.Forms.Label()
        Me.lblBalance = New System.Windows.Forms.Label()
        Me.lblLeaveType = New System.Windows.Forms.Label()
        Me.cmbLeaveType = New System.Windows.Forms.ComboBox()
        Me.txtDateCreated = New System.Windows.Forms.Label()
        Me.lblDateCreated = New System.Windows.Forms.Label()
        Me.lblRoutingStatus = New System.Windows.Forms.Label()
        Me.txtFileId = New System.Windows.Forms.Label()
        Me.lblFileId = New System.Windows.Forms.Label()
        Me.txtRoutingStatus = New System.Windows.Forms.Label()
        Me.btnSave = New PinkieControls.ButtonXP()
        Me.grpEmpInfo = New System.Windows.Forms.GroupBox()
        Me.txtDateHired = New System.Windows.Forms.Label()
        Me.lbDateHired = New System.Windows.Forms.Label()
        Me.txtIdNumber = New System.Windows.Forms.Label()
        Me.lblIdNumber = New System.Windows.Forms.Label()
        Me.txtEmpStatus = New System.Windows.Forms.Label()
        Me.lblEmpStatus = New System.Windows.Forms.Label()
        Me.lblName = New System.Windows.Forms.Label()
        Me.txtPosition = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.Label()
        Me.lblPosition = New System.Windows.Forms.Label()
        Me.txtDepartment = New System.Windows.Forms.Label()
        Me.lblDepartment = New System.Windows.Forms.Label()
        Me.gprLeaveInfo = New System.Windows.Forms.GroupBox()
        Me.lblManagerName = New System.Windows.Forms.Label()
        Me.lblSuperiorName = New System.Windows.Forms.Label()
        Me.lblClinicName = New System.Windows.Forms.Label()
        Me.lblApprovers = New System.Windows.Forms.Label()
        Me.lblManagerStatus = New System.Windows.Forms.Label()
        Me.cmbManagerStatus = New System.Windows.Forms.ComboBox()
        Me.lblManager = New System.Windows.Forms.Label()
        Me.cmbManagerName = New System.Windows.Forms.ComboBox()
        Me.lblManagerPosition = New System.Windows.Forms.Label()
        Me.lblManagerDateApproved = New System.Windows.Forms.Label()
        Me.lblManagerRemarks = New System.Windows.Forms.Label()
        Me.txtManagerDateApproved = New System.Windows.Forms.Label()
        Me.txtManagerRemarks = New System.Windows.Forms.TextBox()
        Me.txtManagerPosition = New System.Windows.Forms.Label()
        Me.lblSuperiorStatus = New System.Windows.Forms.Label()
        Me.cmbSuperiorStatus = New System.Windows.Forms.ComboBox()
        Me.lblSuperior = New System.Windows.Forms.Label()
        Me.cmbSuperiorName = New System.Windows.Forms.ComboBox()
        Me.lblSuperiorPosition = New System.Windows.Forms.Label()
        Me.lblSuperiorDateApproved = New System.Windows.Forms.Label()
        Me.lblSuperiorRemarks = New System.Windows.Forms.Label()
        Me.txtSuperiorDateApproved = New System.Windows.Forms.Label()
        Me.txtSuperiorRemarks = New System.Windows.Forms.TextBox()
        Me.txtSuperiorPosition = New System.Windows.Forms.Label()
        Me.lblClinicStatus = New System.Windows.Forms.Label()
        Me.cmbClinicStatus = New System.Windows.Forms.ComboBox()
        Me.lblClinic = New System.Windows.Forms.Label()
        Me.cmbClinicName = New System.Windows.Forms.ComboBox()
        Me.lblClinicPosition = New System.Windows.Forms.Label()
        Me.lblClinicDateApproved = New System.Windows.Forms.Label()
        Me.lblClinicRemarks = New System.Windows.Forms.Label()
        Me.txtClinicDateApproved = New System.Windows.Forms.Label()
        Me.txtClinicRemarks = New System.Windows.Forms.TextBox()
        Me.txtClinicPosition = New System.Windows.Forms.Label()
        Me.grpEmpInfo.SuspendLayout()
        Me.gprLeaveInfo.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtTotalLeaveCredits
        '
        Me.txtTotalLeaveCredits.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.txtTotalLeaveCredits.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalLeaveCredits.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold)
        Me.txtTotalLeaveCredits.ForeColor = System.Drawing.Color.Black
        Me.txtTotalLeaveCredits.Location = New System.Drawing.Point(852, 28)
        Me.txtTotalLeaveCredits.Name = "txtTotalLeaveCredits"
        Me.txtTotalLeaveCredits.Size = New System.Drawing.Size(70, 24)
        Me.txtTotalLeaveCredits.TabIndex = 304
        Me.txtTotalLeaveCredits.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.txtTotalLeaveCredits.UseCompatibleTextRendering = True
        '
        'lblTotalLeaveCredits
        '
        Me.lblTotalLeaveCredits.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotalLeaveCredits.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalLeaveCredits.ForeColor = System.Drawing.Color.Black
        Me.lblTotalLeaveCredits.Location = New System.Drawing.Point(713, 28)
        Me.lblTotalLeaveCredits.Name = "lblTotalLeaveCredits"
        Me.lblTotalLeaveCredits.Size = New System.Drawing.Size(140, 24)
        Me.lblTotalLeaveCredits.TabIndex = 303
        Me.lblTotalLeaveCredits.Text = " Total Leave Credits"
        Me.lblTotalLeaveCredits.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFrom
        '
        Me.lblFrom.BackColor = System.Drawing.SystemColors.Control
        Me.lblFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFrom.ForeColor = System.Drawing.Color.Black
        Me.lblFrom.Location = New System.Drawing.Point(153, 55)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.Size = New System.Drawing.Size(100, 24)
        Me.lblFrom.TabIndex = 314
        Me.lblFrom.Text = " Start Date"
        Me.lblFrom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dtpFrom
        '
        Me.dtpFrom.CustomFormat = ""
        Me.dtpFrom.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.dtpFrom.Location = New System.Drawing.Point(252, 55)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(280, 24)
        Me.dtpFrom.TabIndex = 1
        '
        'lblTo
        '
        Me.lblTo.BackColor = System.Drawing.SystemColors.Control
        Me.lblTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTo.ForeColor = System.Drawing.Color.Black
        Me.lblTo.Location = New System.Drawing.Point(536, 55)
        Me.lblTo.Name = "lblTo"
        Me.lblTo.Size = New System.Drawing.Size(100, 24)
        Me.lblTo.TabIndex = 316
        Me.lblTo.Text = " End Date"
        Me.lblTo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dtpTo
        '
        Me.dtpTo.CustomFormat = ""
        Me.dtpTo.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.dtpTo.Location = New System.Drawing.Point(635, 55)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(287, 24)
        Me.dtpTo.TabIndex = 2
        '
        'lblDatesOfLeave
        '
        Me.lblDatesOfLeave.BackColor = System.Drawing.SystemColors.Control
        Me.lblDatesOfLeave.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDatesOfLeave.ForeColor = System.Drawing.Color.Black
        Me.lblDatesOfLeave.Location = New System.Drawing.Point(14, 55)
        Me.lblDatesOfLeave.Name = "lblDatesOfLeave"
        Me.lblDatesOfLeave.Size = New System.Drawing.Size(140, 24)
        Me.lblDatesOfLeave.TabIndex = 317
        Me.lblDatesOfLeave.Text = " Date/s of Leave"
        Me.lblDatesOfLeave.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblNumberOfDays
        '
        Me.lblNumberOfDays.BackColor = System.Drawing.SystemColors.Control
        Me.lblNumberOfDays.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNumberOfDays.ForeColor = System.Drawing.Color.Black
        Me.lblNumberOfDays.Location = New System.Drawing.Point(925, 55)
        Me.lblNumberOfDays.Name = "lblNumberOfDays"
        Me.lblNumberOfDays.Size = New System.Drawing.Size(140, 24)
        Me.lblNumberOfDays.TabIndex = 319
        Me.lblNumberOfDays.Text = " No. of Day(s)"
        Me.lblNumberOfDays.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtNumberOfDays
        '
        Me.txtNumberOfDays.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.txtNumberOfDays.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNumberOfDays.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold)
        Me.txtNumberOfDays.ForeColor = System.Drawing.Color.Black
        Me.txtNumberOfDays.Location = New System.Drawing.Point(1064, 55)
        Me.txtNumberOfDays.Name = "txtNumberOfDays"
        Me.txtNumberOfDays.Size = New System.Drawing.Size(100, 24)
        Me.txtNumberOfDays.TabIndex = 320
        Me.txtNumberOfDays.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.txtNumberOfDays.UseCompatibleTextRendering = True
        '
        'lblReason
        '
        Me.lblReason.BackColor = System.Drawing.SystemColors.Control
        Me.lblReason.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblReason.ForeColor = System.Drawing.Color.Black
        Me.lblReason.Location = New System.Drawing.Point(14, 81)
        Me.lblReason.Name = "lblReason"
        Me.lblReason.Size = New System.Drawing.Size(1150, 24)
        Me.lblReason.TabIndex = 321
        Me.lblReason.Text = " Reason for Leave"
        Me.lblReason.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtReason
        '
        Me.txtReason.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.txtReason.Location = New System.Drawing.Point(14, 104)
        Me.txtReason.Multiline = True
        Me.txtReason.Name = "txtReason"
        Me.txtReason.Size = New System.Drawing.Size(1150, 70)
        Me.txtReason.TabIndex = 3
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnClose.DefaultScheme = False
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.btnClose.Hint = ""
        Me.btnClose.Location = New System.Drawing.Point(1079, 603)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Scheme = PinkieControls.ButtonXP.Schemes.Blue
        Me.btnClose.Size = New System.Drawing.Size(100, 32)
        Me.btnClose.TabIndex = 328
        Me.btnClose.TabStop = False
        Me.btnClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnDelete.DefaultScheme = False
        Me.btnDelete.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnDelete.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.btnDelete.Hint = ""
        Me.btnDelete.Location = New System.Drawing.Point(975, 603)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Scheme = PinkieControls.ButtonXP.Schemes.Blue
        Me.btnDelete.Size = New System.Drawing.Size(100, 32)
        Me.btnDelete.TabIndex = 327
        Me.btnDelete.TabStop = False
        Me.btnDelete.Text = "Delete"
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnCancel.DefaultScheme = False
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.btnCancel.Hint = ""
        Me.btnCancel.Location = New System.Drawing.Point(870, 603)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Scheme = PinkieControls.ButtonXP.Schemes.Blue
        Me.btnCancel.Size = New System.Drawing.Size(100, 32)
        Me.btnCancel.TabIndex = 326
        Me.btnCancel.TabStop = False
        Me.btnCancel.Text = "Cancel"
        '
        'txtBalance
        '
        Me.txtBalance.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.txtBalance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBalance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold)
        Me.txtBalance.ForeColor = System.Drawing.Color.Black
        Me.txtBalance.Location = New System.Drawing.Point(1064, 28)
        Me.txtBalance.Name = "txtBalance"
        Me.txtBalance.Size = New System.Drawing.Size(100, 24)
        Me.txtBalance.TabIndex = 359
        Me.txtBalance.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.txtBalance.UseCompatibleTextRendering = True
        '
        'lblBalance
        '
        Me.lblBalance.BackColor = System.Drawing.SystemColors.Control
        Me.lblBalance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBalance.ForeColor = System.Drawing.Color.Black
        Me.lblBalance.Location = New System.Drawing.Point(925, 28)
        Me.lblBalance.Name = "lblBalance"
        Me.lblBalance.Size = New System.Drawing.Size(140, 24)
        Me.lblBalance.TabIndex = 358
        Me.lblBalance.Text = " Balance"
        Me.lblBalance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblLeaveType
        '
        Me.lblLeaveType.BackColor = System.Drawing.SystemColors.Control
        Me.lblLeaveType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLeaveType.ForeColor = System.Drawing.Color.Black
        Me.lblLeaveType.Location = New System.Drawing.Point(14, 28)
        Me.lblLeaveType.Name = "lblLeaveType"
        Me.lblLeaveType.Size = New System.Drawing.Size(140, 24)
        Me.lblLeaveType.TabIndex = 357
        Me.lblLeaveType.Text = " Leave Type"
        Me.lblLeaveType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbLeaveType
        '
        Me.cmbLeaveType.DisplayMember = "Name"
        Me.cmbLeaveType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLeaveType.Font = New System.Drawing.Font("Verdana", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbLeaveType.FormattingEnabled = True
        Me.cmbLeaveType.Items.AddRange(New Object() {"Vacation Leave", "Sick Leave"})
        Me.cmbLeaveType.Location = New System.Drawing.Point(153, 28)
        Me.cmbLeaveType.Name = "cmbLeaveType"
        Me.cmbLeaveType.Size = New System.Drawing.Size(556, 24)
        Me.cmbLeaveType.TabIndex = 0
        Me.cmbLeaveType.ValueMember = "Id"
        '
        'txtDateCreated
        '
        Me.txtDateCreated.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDateCreated.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.txtDateCreated.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDateCreated.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.txtDateCreated.ForeColor = System.Drawing.Color.Black
        Me.txtDateCreated.Location = New System.Drawing.Point(973, 9)
        Me.txtDateCreated.Name = "txtDateCreated"
        Me.txtDateCreated.Size = New System.Drawing.Size(200, 24)
        Me.txtDateCreated.TabIndex = 363
        Me.txtDateCreated.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.txtDateCreated.UseCompatibleTextRendering = True
        '
        'lblDateCreated
        '
        Me.lblDateCreated.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDateCreated.BackColor = System.Drawing.SystemColors.Control
        Me.lblDateCreated.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDateCreated.ForeColor = System.Drawing.Color.Black
        Me.lblDateCreated.Location = New System.Drawing.Point(874, 9)
        Me.lblDateCreated.Name = "lblDateCreated"
        Me.lblDateCreated.Size = New System.Drawing.Size(100, 24)
        Me.lblDateCreated.TabIndex = 362
        Me.lblDateCreated.Text = " Date Created"
        Me.lblDateCreated.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRoutingStatus
        '
        Me.lblRoutingStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblRoutingStatus.BackColor = System.Drawing.SystemColors.Control
        Me.lblRoutingStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblRoutingStatus.ForeColor = System.Drawing.Color.Black
        Me.lblRoutingStatus.Location = New System.Drawing.Point(525, 9)
        Me.lblRoutingStatus.Name = "lblRoutingStatus"
        Me.lblRoutingStatus.Size = New System.Drawing.Size(100, 24)
        Me.lblRoutingStatus.TabIndex = 368
        Me.lblRoutingStatus.Text = " File Status"
        Me.lblRoutingStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtFileId
        '
        Me.txtFileId.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.txtFileId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFileId.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.txtFileId.ForeColor = System.Drawing.Color.Black
        Me.txtFileId.Location = New System.Drawing.Point(78, 9)
        Me.txtFileId.Name = "txtFileId"
        Me.txtFileId.Size = New System.Drawing.Size(100, 24)
        Me.txtFileId.TabIndex = 383
        Me.txtFileId.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.txtFileId.UseCompatibleTextRendering = True
        '
        'lblFileId
        '
        Me.lblFileId.BackColor = System.Drawing.SystemColors.Control
        Me.lblFileId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFileId.ForeColor = System.Drawing.Color.Black
        Me.lblFileId.Location = New System.Drawing.Point(9, 9)
        Me.lblFileId.Name = "lblFileId"
        Me.lblFileId.Size = New System.Drawing.Size(70, 24)
        Me.lblFileId.TabIndex = 382
        Me.lblFileId.Text = " File ID"
        Me.lblFileId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRoutingStatus
        '
        Me.txtRoutingStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtRoutingStatus.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.txtRoutingStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRoutingStatus.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.txtRoutingStatus.ForeColor = System.Drawing.Color.Black
        Me.txtRoutingStatus.Location = New System.Drawing.Point(624, 9)
        Me.txtRoutingStatus.Name = "txtRoutingStatus"
        Me.txtRoutingStatus.Size = New System.Drawing.Size(247, 24)
        Me.txtRoutingStatus.TabIndex = 385
        Me.txtRoutingStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtRoutingStatus.UseCompatibleTextRendering = True
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnSave.DefaultScheme = False
        Me.btnSave.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnSave.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.btnSave.Hint = ""
        Me.btnSave.Location = New System.Drawing.Point(765, 603)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Scheme = PinkieControls.ButtonXP.Schemes.Blue
        Me.btnSave.Size = New System.Drawing.Size(100, 32)
        Me.btnSave.TabIndex = 325
        Me.btnSave.TabStop = False
        Me.btnSave.Text = "Save"
        '
        'grpEmpInfo
        '
        Me.grpEmpInfo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpEmpInfo.Controls.Add(Me.txtDateHired)
        Me.grpEmpInfo.Controls.Add(Me.lbDateHired)
        Me.grpEmpInfo.Controls.Add(Me.txtIdNumber)
        Me.grpEmpInfo.Controls.Add(Me.lblIdNumber)
        Me.grpEmpInfo.Controls.Add(Me.txtEmpStatus)
        Me.grpEmpInfo.Controls.Add(Me.lblEmpStatus)
        Me.grpEmpInfo.Controls.Add(Me.lblName)
        Me.grpEmpInfo.Controls.Add(Me.txtPosition)
        Me.grpEmpInfo.Controls.Add(Me.lblPosition)
        Me.grpEmpInfo.Controls.Add(Me.txtDepartment)
        Me.grpEmpInfo.Controls.Add(Me.lblDepartment)
        Me.grpEmpInfo.Location = New System.Drawing.Point(9, 38)
        Me.grpEmpInfo.Name = "grpEmpInfo"
        Me.grpEmpInfo.Size = New System.Drawing.Size(1178, 120)
        Me.grpEmpInfo.TabIndex = 401
        Me.grpEmpInfo.TabStop = False
        Me.grpEmpInfo.Text = "Employee Information"
        '
        'txtDateHired
        '
        Me.txtDateHired.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.txtDateHired.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDateHired.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.txtDateHired.ForeColor = System.Drawing.Color.Black
        Me.txtDateHired.Location = New System.Drawing.Point(767, 79)
        Me.txtDateHired.Name = "txtDateHired"
        Me.txtDateHired.Size = New System.Drawing.Size(397, 24)
        Me.txtDateHired.TabIndex = 413
        Me.txtDateHired.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtDateHired.UseCompatibleTextRendering = True
        '
        'lbDateHired
        '
        Me.lbDateHired.BackColor = System.Drawing.SystemColors.Control
        Me.lbDateHired.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbDateHired.ForeColor = System.Drawing.Color.Black
        Me.lbDateHired.Location = New System.Drawing.Point(629, 79)
        Me.lbDateHired.Name = "lbDateHired"
        Me.lbDateHired.Size = New System.Drawing.Size(140, 24)
        Me.lbDateHired.TabIndex = 412
        Me.lbDateHired.Text = " Date Hired"
        Me.lbDateHired.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtIdNumber
        '
        Me.txtIdNumber.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.txtIdNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIdNumber.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.txtIdNumber.ForeColor = System.Drawing.Color.Black
        Me.txtIdNumber.Location = New System.Drawing.Point(153, 53)
        Me.txtIdNumber.Name = "txtIdNumber"
        Me.txtIdNumber.Size = New System.Drawing.Size(473, 24)
        Me.txtIdNumber.TabIndex = 411
        Me.txtIdNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtIdNumber.UseCompatibleTextRendering = True
        '
        'lblIdNumber
        '
        Me.lblIdNumber.BackColor = System.Drawing.SystemColors.Control
        Me.lblIdNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblIdNumber.ForeColor = System.Drawing.Color.Black
        Me.lblIdNumber.Location = New System.Drawing.Point(14, 53)
        Me.lblIdNumber.Name = "lblIdNumber"
        Me.lblIdNumber.Size = New System.Drawing.Size(140, 24)
        Me.lblIdNumber.TabIndex = 410
        Me.lblIdNumber.Text = " ID Number"
        Me.lblIdNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtEmpStatus
        '
        Me.txtEmpStatus.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.txtEmpStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmpStatus.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.txtEmpStatus.ForeColor = System.Drawing.Color.Black
        Me.txtEmpStatus.Location = New System.Drawing.Point(767, 53)
        Me.txtEmpStatus.Name = "txtEmpStatus"
        Me.txtEmpStatus.Size = New System.Drawing.Size(397, 24)
        Me.txtEmpStatus.TabIndex = 409
        Me.txtEmpStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtEmpStatus.UseCompatibleTextRendering = True
        '
        'lblEmpStatus
        '
        Me.lblEmpStatus.BackColor = System.Drawing.SystemColors.Control
        Me.lblEmpStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblEmpStatus.ForeColor = System.Drawing.Color.Black
        Me.lblEmpStatus.Location = New System.Drawing.Point(629, 53)
        Me.lblEmpStatus.Name = "lblEmpStatus"
        Me.lblEmpStatus.Size = New System.Drawing.Size(140, 24)
        Me.lblEmpStatus.TabIndex = 408
        Me.lblEmpStatus.Text = " Emp. Status"
        Me.lblEmpStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblName
        '
        Me.lblName.BackColor = System.Drawing.SystemColors.Control
        Me.lblName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblName.ForeColor = System.Drawing.Color.Black
        Me.lblName.Location = New System.Drawing.Point(14, 27)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(140, 24)
        Me.lblName.TabIndex = 401
        Me.lblName.Text = " Fullname"
        Me.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtPosition
        '
        Me.txtPosition.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.txtPosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPosition.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.txtPosition.ForeColor = System.Drawing.Color.Black
        Me.txtPosition.Location = New System.Drawing.Point(767, 27)
        Me.txtPosition.Name = "txtPosition"
        Me.txtPosition.Size = New System.Drawing.Size(397, 24)
        Me.txtPosition.TabIndex = 407
        Me.txtPosition.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtPosition.UseCompatibleTextRendering = True
        '
        'txtName
        '
        Me.txtName.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtName.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.txtName.ForeColor = System.Drawing.Color.Black
        Me.txtName.Location = New System.Drawing.Point(162, 65)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(473, 24)
        Me.txtName.TabIndex = 402
        Me.txtName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtName.UseCompatibleTextRendering = True
        '
        'lblPosition
        '
        Me.lblPosition.BackColor = System.Drawing.SystemColors.Control
        Me.lblPosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPosition.ForeColor = System.Drawing.Color.Black
        Me.lblPosition.Location = New System.Drawing.Point(629, 27)
        Me.lblPosition.Name = "lblPosition"
        Me.lblPosition.Size = New System.Drawing.Size(140, 24)
        Me.lblPosition.TabIndex = 406
        Me.lblPosition.Text = " Position"
        Me.lblPosition.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDepartment
        '
        Me.txtDepartment.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.txtDepartment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDepartment.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.txtDepartment.ForeColor = System.Drawing.Color.Black
        Me.txtDepartment.Location = New System.Drawing.Point(153, 79)
        Me.txtDepartment.Name = "txtDepartment"
        Me.txtDepartment.Size = New System.Drawing.Size(473, 24)
        Me.txtDepartment.TabIndex = 405
        Me.txtDepartment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtDepartment.UseCompatibleTextRendering = True
        '
        'lblDepartment
        '
        Me.lblDepartment.BackColor = System.Drawing.SystemColors.Control
        Me.lblDepartment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDepartment.ForeColor = System.Drawing.Color.Black
        Me.lblDepartment.Location = New System.Drawing.Point(14, 79)
        Me.lblDepartment.Name = "lblDepartment"
        Me.lblDepartment.Size = New System.Drawing.Size(140, 24)
        Me.lblDepartment.TabIndex = 404
        Me.lblDepartment.Text = " Department"
        Me.lblDepartment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'gprLeaveInfo
        '
        Me.gprLeaveInfo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gprLeaveInfo.Controls.Add(Me.lblTo)
        Me.gprLeaveInfo.Controls.Add(Me.lblFrom)
        Me.gprLeaveInfo.Controls.Add(Me.lblManagerName)
        Me.gprLeaveInfo.Controls.Add(Me.lblSuperiorName)
        Me.gprLeaveInfo.Controls.Add(Me.lblClinicName)
        Me.gprLeaveInfo.Controls.Add(Me.lblApprovers)
        Me.gprLeaveInfo.Controls.Add(Me.lblManagerStatus)
        Me.gprLeaveInfo.Controls.Add(Me.cmbManagerStatus)
        Me.gprLeaveInfo.Controls.Add(Me.lblManager)
        Me.gprLeaveInfo.Controls.Add(Me.cmbManagerName)
        Me.gprLeaveInfo.Controls.Add(Me.lblManagerPosition)
        Me.gprLeaveInfo.Controls.Add(Me.lblManagerDateApproved)
        Me.gprLeaveInfo.Controls.Add(Me.lblManagerRemarks)
        Me.gprLeaveInfo.Controls.Add(Me.txtManagerDateApproved)
        Me.gprLeaveInfo.Controls.Add(Me.txtManagerRemarks)
        Me.gprLeaveInfo.Controls.Add(Me.txtManagerPosition)
        Me.gprLeaveInfo.Controls.Add(Me.lblSuperiorStatus)
        Me.gprLeaveInfo.Controls.Add(Me.cmbSuperiorStatus)
        Me.gprLeaveInfo.Controls.Add(Me.lblSuperior)
        Me.gprLeaveInfo.Controls.Add(Me.cmbSuperiorName)
        Me.gprLeaveInfo.Controls.Add(Me.lblSuperiorPosition)
        Me.gprLeaveInfo.Controls.Add(Me.lblSuperiorDateApproved)
        Me.gprLeaveInfo.Controls.Add(Me.lblSuperiorRemarks)
        Me.gprLeaveInfo.Controls.Add(Me.txtSuperiorDateApproved)
        Me.gprLeaveInfo.Controls.Add(Me.txtSuperiorRemarks)
        Me.gprLeaveInfo.Controls.Add(Me.txtSuperiorPosition)
        Me.gprLeaveInfo.Controls.Add(Me.lblClinicStatus)
        Me.gprLeaveInfo.Controls.Add(Me.cmbClinicStatus)
        Me.gprLeaveInfo.Controls.Add(Me.lblClinic)
        Me.gprLeaveInfo.Controls.Add(Me.cmbClinicName)
        Me.gprLeaveInfo.Controls.Add(Me.lblClinicPosition)
        Me.gprLeaveInfo.Controls.Add(Me.lblClinicDateApproved)
        Me.gprLeaveInfo.Controls.Add(Me.lblClinicRemarks)
        Me.gprLeaveInfo.Controls.Add(Me.txtClinicDateApproved)
        Me.gprLeaveInfo.Controls.Add(Me.txtClinicRemarks)
        Me.gprLeaveInfo.Controls.Add(Me.txtClinicPosition)
        Me.gprLeaveInfo.Controls.Add(Me.lblLeaveType)
        Me.gprLeaveInfo.Controls.Add(Me.cmbLeaveType)
        Me.gprLeaveInfo.Controls.Add(Me.txtTotalLeaveCredits)
        Me.gprLeaveInfo.Controls.Add(Me.lblTotalLeaveCredits)
        Me.gprLeaveInfo.Controls.Add(Me.txtBalance)
        Me.gprLeaveInfo.Controls.Add(Me.lblBalance)
        Me.gprLeaveInfo.Controls.Add(Me.dtpTo)
        Me.gprLeaveInfo.Controls.Add(Me.dtpFrom)
        Me.gprLeaveInfo.Controls.Add(Me.lblDatesOfLeave)
        Me.gprLeaveInfo.Controls.Add(Me.lblNumberOfDays)
        Me.gprLeaveInfo.Controls.Add(Me.txtReason)
        Me.gprLeaveInfo.Controls.Add(Me.lblReason)
        Me.gprLeaveInfo.Controls.Add(Me.txtNumberOfDays)
        Me.gprLeaveInfo.Location = New System.Drawing.Point(9, 164)
        Me.gprLeaveInfo.Name = "gprLeaveInfo"
        Me.gprLeaveInfo.Size = New System.Drawing.Size(1178, 430)
        Me.gprLeaveInfo.TabIndex = 402
        Me.gprLeaveInfo.TabStop = False
        Me.gprLeaveInfo.Text = "Fill-up Form"
        '
        'lblManagerName
        '
        Me.lblManagerName.BackColor = System.Drawing.SystemColors.Control
        Me.lblManagerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblManagerName.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblManagerName.ForeColor = System.Drawing.Color.Black
        Me.lblManagerName.Location = New System.Drawing.Point(786, 262)
        Me.lblManagerName.Name = "lblManagerName"
        Me.lblManagerName.Size = New System.Drawing.Size(85, 24)
        Me.lblManagerName.TabIndex = 509
        Me.lblManagerName.Text = " Name"
        Me.lblManagerName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSuperiorName
        '
        Me.lblSuperiorName.BackColor = System.Drawing.SystemColors.Control
        Me.lblSuperiorName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSuperiorName.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSuperiorName.ForeColor = System.Drawing.Color.Black
        Me.lblSuperiorName.Location = New System.Drawing.Point(397, 262)
        Me.lblSuperiorName.Name = "lblSuperiorName"
        Me.lblSuperiorName.Size = New System.Drawing.Size(85, 24)
        Me.lblSuperiorName.TabIndex = 498
        Me.lblSuperiorName.Text = " Name"
        Me.lblSuperiorName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblClinicName
        '
        Me.lblClinicName.BackColor = System.Drawing.SystemColors.Control
        Me.lblClinicName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblClinicName.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClinicName.ForeColor = System.Drawing.Color.Black
        Me.lblClinicName.Location = New System.Drawing.Point(8, 262)
        Me.lblClinicName.Name = "lblClinicName"
        Me.lblClinicName.Size = New System.Drawing.Size(85, 24)
        Me.lblClinicName.TabIndex = 487
        Me.lblClinicName.Text = " Name"
        Me.lblClinicName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblApprovers
        '
        Me.lblApprovers.AutoSize = True
        Me.lblApprovers.Location = New System.Drawing.Point(6, 186)
        Me.lblApprovers.Name = "lblApprovers"
        Me.lblApprovers.Size = New System.Drawing.Size(71, 14)
        Me.lblApprovers.TabIndex = 403
        Me.lblApprovers.Text = "Approvers"
        '
        'lblManagerStatus
        '
        Me.lblManagerStatus.BackColor = System.Drawing.SystemColors.Control
        Me.lblManagerStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblManagerStatus.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblManagerStatus.ForeColor = System.Drawing.Color.Black
        Me.lblManagerStatus.Location = New System.Drawing.Point(786, 234)
        Me.lblManagerStatus.Name = "lblManagerStatus"
        Me.lblManagerStatus.Size = New System.Drawing.Size(85, 24)
        Me.lblManagerStatus.TabIndex = 518
        Me.lblManagerStatus.Text = " Status"
        Me.lblManagerStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbManagerStatus
        '
        Me.cmbManagerStatus.DisplayMember = "Name"
        Me.cmbManagerStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbManagerStatus.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbManagerStatus.FormattingEnabled = True
        Me.cmbManagerStatus.Items.AddRange(New Object() {"< Not Set >", "Approved", "Disapproved"})
        Me.cmbManagerStatus.Location = New System.Drawing.Point(870, 234)
        Me.cmbManagerStatus.Name = "cmbManagerStatus"
        Me.cmbManagerStatus.Size = New System.Drawing.Size(300, 24)
        Me.cmbManagerStatus.TabIndex = 517
        Me.cmbManagerStatus.ValueMember = "Id"
        '
        'lblManager
        '
        Me.lblManager.BackColor = System.Drawing.SystemColors.Control
        Me.lblManager.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblManager.ForeColor = System.Drawing.Color.Black
        Me.lblManager.Location = New System.Drawing.Point(786, 207)
        Me.lblManager.Name = "lblManager"
        Me.lblManager.Size = New System.Drawing.Size(384, 24)
        Me.lblManager.TabIndex = 514
        Me.lblManager.Text = "Manager"
        Me.lblManager.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmbManagerName
        '
        Me.cmbManagerName.DisplayMember = "Name"
        Me.cmbManagerName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbManagerName.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbManagerName.FormattingEnabled = True
        Me.cmbManagerName.Items.AddRange(New Object() {"BARTOLOME, NICOLE KATE CASTILLO"})
        Me.cmbManagerName.Location = New System.Drawing.Point(870, 262)
        Me.cmbManagerName.Name = "cmbManagerName"
        Me.cmbManagerName.Size = New System.Drawing.Size(300, 24)
        Me.cmbManagerName.TabIndex = 508
        Me.cmbManagerName.ValueMember = "Id"
        '
        'lblManagerPosition
        '
        Me.lblManagerPosition.BackColor = System.Drawing.SystemColors.Control
        Me.lblManagerPosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblManagerPosition.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblManagerPosition.ForeColor = System.Drawing.Color.Black
        Me.lblManagerPosition.Location = New System.Drawing.Point(786, 289)
        Me.lblManagerPosition.Name = "lblManagerPosition"
        Me.lblManagerPosition.Size = New System.Drawing.Size(85, 24)
        Me.lblManagerPosition.TabIndex = 510
        Me.lblManagerPosition.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblManagerDateApproved
        '
        Me.lblManagerDateApproved.BackColor = System.Drawing.SystemColors.Control
        Me.lblManagerDateApproved.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblManagerDateApproved.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblManagerDateApproved.ForeColor = System.Drawing.Color.Black
        Me.lblManagerDateApproved.Location = New System.Drawing.Point(786, 316)
        Me.lblManagerDateApproved.Name = "lblManagerDateApproved"
        Me.lblManagerDateApproved.Size = New System.Drawing.Size(85, 24)
        Me.lblManagerDateApproved.TabIndex = 511
        Me.lblManagerDateApproved.Text = " Date"
        Me.lblManagerDateApproved.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblManagerRemarks
        '
        Me.lblManagerRemarks.BackColor = System.Drawing.SystemColors.Control
        Me.lblManagerRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblManagerRemarks.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblManagerRemarks.ForeColor = System.Drawing.Color.Black
        Me.lblManagerRemarks.Location = New System.Drawing.Point(786, 343)
        Me.lblManagerRemarks.Name = "lblManagerRemarks"
        Me.lblManagerRemarks.Size = New System.Drawing.Size(384, 24)
        Me.lblManagerRemarks.TabIndex = 516
        Me.lblManagerRemarks.Text = " Remarks"
        Me.lblManagerRemarks.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtManagerDateApproved
        '
        Me.txtManagerDateApproved.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.txtManagerDateApproved.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtManagerDateApproved.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.txtManagerDateApproved.ForeColor = System.Drawing.Color.Black
        Me.txtManagerDateApproved.Location = New System.Drawing.Point(870, 316)
        Me.txtManagerDateApproved.Name = "txtManagerDateApproved"
        Me.txtManagerDateApproved.Size = New System.Drawing.Size(300, 24)
        Me.txtManagerDateApproved.TabIndex = 512
        Me.txtManagerDateApproved.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtManagerDateApproved.UseCompatibleTextRendering = True
        '
        'txtManagerRemarks
        '
        Me.txtManagerRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtManagerRemarks.Location = New System.Drawing.Point(786, 366)
        Me.txtManagerRemarks.Multiline = True
        Me.txtManagerRemarks.Name = "txtManagerRemarks"
        Me.txtManagerRemarks.Size = New System.Drawing.Size(384, 53)
        Me.txtManagerRemarks.TabIndex = 515
        '
        'txtManagerPosition
        '
        Me.txtManagerPosition.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.txtManagerPosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtManagerPosition.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.txtManagerPosition.ForeColor = System.Drawing.Color.Black
        Me.txtManagerPosition.Location = New System.Drawing.Point(870, 289)
        Me.txtManagerPosition.Name = "txtManagerPosition"
        Me.txtManagerPosition.Size = New System.Drawing.Size(300, 24)
        Me.txtManagerPosition.TabIndex = 513
        Me.txtManagerPosition.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtManagerPosition.UseCompatibleTextRendering = True
        '
        'lblSuperiorStatus
        '
        Me.lblSuperiorStatus.BackColor = System.Drawing.SystemColors.Control
        Me.lblSuperiorStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSuperiorStatus.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSuperiorStatus.ForeColor = System.Drawing.Color.Black
        Me.lblSuperiorStatus.Location = New System.Drawing.Point(397, 234)
        Me.lblSuperiorStatus.Name = "lblSuperiorStatus"
        Me.lblSuperiorStatus.Size = New System.Drawing.Size(85, 24)
        Me.lblSuperiorStatus.TabIndex = 507
        Me.lblSuperiorStatus.Text = " Status"
        Me.lblSuperiorStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbSuperiorStatus
        '
        Me.cmbSuperiorStatus.DisplayMember = "Name"
        Me.cmbSuperiorStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSuperiorStatus.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSuperiorStatus.FormattingEnabled = True
        Me.cmbSuperiorStatus.Items.AddRange(New Object() {"< Not Set >", "Approved", "Disapproved"})
        Me.cmbSuperiorStatus.Location = New System.Drawing.Point(481, 234)
        Me.cmbSuperiorStatus.Name = "cmbSuperiorStatus"
        Me.cmbSuperiorStatus.Size = New System.Drawing.Size(300, 24)
        Me.cmbSuperiorStatus.TabIndex = 506
        Me.cmbSuperiorStatus.ValueMember = "Id"
        '
        'lblSuperior
        '
        Me.lblSuperior.BackColor = System.Drawing.SystemColors.Control
        Me.lblSuperior.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSuperior.ForeColor = System.Drawing.Color.Black
        Me.lblSuperior.Location = New System.Drawing.Point(397, 207)
        Me.lblSuperior.Name = "lblSuperior"
        Me.lblSuperior.Size = New System.Drawing.Size(384, 24)
        Me.lblSuperior.TabIndex = 503
        Me.lblSuperior.Text = "Immediate Superior"
        Me.lblSuperior.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmbSuperiorName
        '
        Me.cmbSuperiorName.DisplayMember = "Name"
        Me.cmbSuperiorName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSuperiorName.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSuperiorName.FormattingEnabled = True
        Me.cmbSuperiorName.Location = New System.Drawing.Point(481, 262)
        Me.cmbSuperiorName.Name = "cmbSuperiorName"
        Me.cmbSuperiorName.Size = New System.Drawing.Size(300, 24)
        Me.cmbSuperiorName.TabIndex = 497
        Me.cmbSuperiorName.ValueMember = "Id"
        '
        'lblSuperiorPosition
        '
        Me.lblSuperiorPosition.BackColor = System.Drawing.SystemColors.Control
        Me.lblSuperiorPosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSuperiorPosition.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSuperiorPosition.ForeColor = System.Drawing.Color.Black
        Me.lblSuperiorPosition.Location = New System.Drawing.Point(396, 289)
        Me.lblSuperiorPosition.Name = "lblSuperiorPosition"
        Me.lblSuperiorPosition.Size = New System.Drawing.Size(85, 24)
        Me.lblSuperiorPosition.TabIndex = 499
        Me.lblSuperiorPosition.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSuperiorDateApproved
        '
        Me.lblSuperiorDateApproved.BackColor = System.Drawing.SystemColors.Control
        Me.lblSuperiorDateApproved.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSuperiorDateApproved.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSuperiorDateApproved.ForeColor = System.Drawing.Color.Black
        Me.lblSuperiorDateApproved.Location = New System.Drawing.Point(396, 316)
        Me.lblSuperiorDateApproved.Name = "lblSuperiorDateApproved"
        Me.lblSuperiorDateApproved.Size = New System.Drawing.Size(85, 24)
        Me.lblSuperiorDateApproved.TabIndex = 500
        Me.lblSuperiorDateApproved.Text = " Date"
        Me.lblSuperiorDateApproved.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSuperiorRemarks
        '
        Me.lblSuperiorRemarks.BackColor = System.Drawing.SystemColors.Control
        Me.lblSuperiorRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSuperiorRemarks.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSuperiorRemarks.ForeColor = System.Drawing.Color.Black
        Me.lblSuperiorRemarks.Location = New System.Drawing.Point(397, 343)
        Me.lblSuperiorRemarks.Name = "lblSuperiorRemarks"
        Me.lblSuperiorRemarks.Size = New System.Drawing.Size(384, 24)
        Me.lblSuperiorRemarks.TabIndex = 505
        Me.lblSuperiorRemarks.Text = " Remarks"
        Me.lblSuperiorRemarks.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSuperiorDateApproved
        '
        Me.txtSuperiorDateApproved.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.txtSuperiorDateApproved.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSuperiorDateApproved.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.txtSuperiorDateApproved.ForeColor = System.Drawing.Color.Black
        Me.txtSuperiorDateApproved.Location = New System.Drawing.Point(480, 316)
        Me.txtSuperiorDateApproved.Name = "txtSuperiorDateApproved"
        Me.txtSuperiorDateApproved.Size = New System.Drawing.Size(300, 24)
        Me.txtSuperiorDateApproved.TabIndex = 501
        Me.txtSuperiorDateApproved.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSuperiorDateApproved.UseCompatibleTextRendering = True
        '
        'txtSuperiorRemarks
        '
        Me.txtSuperiorRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSuperiorRemarks.Location = New System.Drawing.Point(397, 366)
        Me.txtSuperiorRemarks.Multiline = True
        Me.txtSuperiorRemarks.Name = "txtSuperiorRemarks"
        Me.txtSuperiorRemarks.Size = New System.Drawing.Size(384, 53)
        Me.txtSuperiorRemarks.TabIndex = 504
        '
        'txtSuperiorPosition
        '
        Me.txtSuperiorPosition.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.txtSuperiorPosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSuperiorPosition.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.txtSuperiorPosition.ForeColor = System.Drawing.Color.Black
        Me.txtSuperiorPosition.Location = New System.Drawing.Point(480, 289)
        Me.txtSuperiorPosition.Name = "txtSuperiorPosition"
        Me.txtSuperiorPosition.Size = New System.Drawing.Size(300, 24)
        Me.txtSuperiorPosition.TabIndex = 502
        Me.txtSuperiorPosition.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSuperiorPosition.UseCompatibleTextRendering = True
        '
        'lblClinicStatus
        '
        Me.lblClinicStatus.BackColor = System.Drawing.SystemColors.Control
        Me.lblClinicStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblClinicStatus.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClinicStatus.ForeColor = System.Drawing.Color.Black
        Me.lblClinicStatus.Location = New System.Drawing.Point(8, 235)
        Me.lblClinicStatus.Name = "lblClinicStatus"
        Me.lblClinicStatus.Size = New System.Drawing.Size(85, 24)
        Me.lblClinicStatus.TabIndex = 496
        Me.lblClinicStatus.Text = " Status"
        Me.lblClinicStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbClinicStatus
        '
        Me.cmbClinicStatus.DisplayMember = "Name"
        Me.cmbClinicStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbClinicStatus.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbClinicStatus.FormattingEnabled = True
        Me.cmbClinicStatus.Items.AddRange(New Object() {"< Not Set >", "Approved", "Disapproved"})
        Me.cmbClinicStatus.Location = New System.Drawing.Point(92, 235)
        Me.cmbClinicStatus.Name = "cmbClinicStatus"
        Me.cmbClinicStatus.Size = New System.Drawing.Size(300, 24)
        Me.cmbClinicStatus.TabIndex = 495
        Me.cmbClinicStatus.ValueMember = "Id"
        '
        'lblClinic
        '
        Me.lblClinic.BackColor = System.Drawing.SystemColors.Control
        Me.lblClinic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblClinic.ForeColor = System.Drawing.Color.Black
        Me.lblClinic.Location = New System.Drawing.Point(8, 208)
        Me.lblClinic.Name = "lblClinic"
        Me.lblClinic.Size = New System.Drawing.Size(384, 24)
        Me.lblClinic.TabIndex = 492
        Me.lblClinic.Text = "Clinic"
        Me.lblClinic.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmbClinicName
        '
        Me.cmbClinicName.DisplayMember = "Name"
        Me.cmbClinicName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbClinicName.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbClinicName.FormattingEnabled = True
        Me.cmbClinicName.Location = New System.Drawing.Point(92, 262)
        Me.cmbClinicName.Name = "cmbClinicName"
        Me.cmbClinicName.Size = New System.Drawing.Size(300, 24)
        Me.cmbClinicName.TabIndex = 486
        Me.cmbClinicName.ValueMember = "Id"
        '
        'lblClinicPosition
        '
        Me.lblClinicPosition.BackColor = System.Drawing.SystemColors.Control
        Me.lblClinicPosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblClinicPosition.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClinicPosition.ForeColor = System.Drawing.Color.Black
        Me.lblClinicPosition.Location = New System.Drawing.Point(8, 289)
        Me.lblClinicPosition.Name = "lblClinicPosition"
        Me.lblClinicPosition.Size = New System.Drawing.Size(85, 24)
        Me.lblClinicPosition.TabIndex = 488
        Me.lblClinicPosition.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblClinicDateApproved
        '
        Me.lblClinicDateApproved.BackColor = System.Drawing.SystemColors.Control
        Me.lblClinicDateApproved.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblClinicDateApproved.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClinicDateApproved.ForeColor = System.Drawing.Color.Black
        Me.lblClinicDateApproved.Location = New System.Drawing.Point(8, 316)
        Me.lblClinicDateApproved.Name = "lblClinicDateApproved"
        Me.lblClinicDateApproved.Size = New System.Drawing.Size(85, 24)
        Me.lblClinicDateApproved.TabIndex = 489
        Me.lblClinicDateApproved.Text = " Date"
        Me.lblClinicDateApproved.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblClinicRemarks
        '
        Me.lblClinicRemarks.BackColor = System.Drawing.SystemColors.Control
        Me.lblClinicRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblClinicRemarks.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClinicRemarks.ForeColor = System.Drawing.Color.Black
        Me.lblClinicRemarks.Location = New System.Drawing.Point(8, 343)
        Me.lblClinicRemarks.Name = "lblClinicRemarks"
        Me.lblClinicRemarks.Size = New System.Drawing.Size(384, 24)
        Me.lblClinicRemarks.TabIndex = 494
        Me.lblClinicRemarks.Text = " Remarks"
        Me.lblClinicRemarks.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtClinicDateApproved
        '
        Me.txtClinicDateApproved.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.txtClinicDateApproved.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtClinicDateApproved.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.txtClinicDateApproved.ForeColor = System.Drawing.Color.Black
        Me.txtClinicDateApproved.Location = New System.Drawing.Point(92, 316)
        Me.txtClinicDateApproved.Name = "txtClinicDateApproved"
        Me.txtClinicDateApproved.Size = New System.Drawing.Size(300, 24)
        Me.txtClinicDateApproved.TabIndex = 490
        Me.txtClinicDateApproved.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtClinicDateApproved.UseCompatibleTextRendering = True
        '
        'txtClinicRemarks
        '
        Me.txtClinicRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtClinicRemarks.Location = New System.Drawing.Point(8, 366)
        Me.txtClinicRemarks.Multiline = True
        Me.txtClinicRemarks.Name = "txtClinicRemarks"
        Me.txtClinicRemarks.Size = New System.Drawing.Size(384, 53)
        Me.txtClinicRemarks.TabIndex = 493
        '
        'txtClinicPosition
        '
        Me.txtClinicPosition.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.txtClinicPosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtClinicPosition.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.txtClinicPosition.ForeColor = System.Drawing.Color.Black
        Me.txtClinicPosition.Location = New System.Drawing.Point(92, 289)
        Me.txtClinicPosition.Name = "txtClinicPosition"
        Me.txtClinicPosition.Size = New System.Drawing.Size(300, 24)
        Me.txtClinicPosition.TabIndex = 491
        Me.txtClinicPosition.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtClinicPosition.UseCompatibleTextRendering = True
        '
        'frmLeaveFiling
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(1196, 647)
        Me.Controls.Add(Me.gprLeaveInfo)
        Me.Controls.Add(Me.lblDateCreated)
        Me.Controls.Add(Me.txtDateCreated)
        Me.Controls.Add(Me.lblRoutingStatus)
        Me.Controls.Add(Me.lblFileId)
        Me.Controls.Add(Me.txtFileId)
        Me.Controls.Add(Me.txtRoutingStatus)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.grpEmpInfo)
        Me.Font = New System.Drawing.Font("Verdana", 8.5!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLeaveFiling"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Leave Entry Form"
        Me.grpEmpInfo.ResumeLayout(False)
        Me.gprLeaveInfo.ResumeLayout(False)
        Me.gprLeaveInfo.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtTotalLeaveCredits As System.Windows.Forms.Label
    Friend WithEvents lblTotalLeaveCredits As System.Windows.Forms.Label
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Public WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblTo As System.Windows.Forms.Label
    Public WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblDatesOfLeave As System.Windows.Forms.Label
    Friend WithEvents lblNumberOfDays As System.Windows.Forms.Label
    Friend WithEvents txtNumberOfDays As System.Windows.Forms.Label
    Friend WithEvents lblReason As System.Windows.Forms.Label
    Friend WithEvents txtReason As System.Windows.Forms.TextBox
    Friend WithEvents btnClose As PinkieControls.ButtonXP
    Friend WithEvents btnDelete As PinkieControls.ButtonXP
    Friend WithEvents btnCancel As PinkieControls.ButtonXP
    Friend WithEvents txtBalance As System.Windows.Forms.Label
    Friend WithEvents lblBalance As System.Windows.Forms.Label
    Friend WithEvents lblLeaveType As System.Windows.Forms.Label
    Friend WithEvents cmbLeaveType As System.Windows.Forms.ComboBox
    Friend WithEvents txtDateCreated As System.Windows.Forms.Label
    Friend WithEvents lblDateCreated As System.Windows.Forms.Label
    Friend WithEvents lblRoutingStatus As System.Windows.Forms.Label
    Friend WithEvents txtFileId As System.Windows.Forms.Label
    Friend WithEvents lblFileId As System.Windows.Forms.Label
    Friend WithEvents txtRoutingStatus As System.Windows.Forms.Label
    Friend WithEvents btnSave As PinkieControls.ButtonXP
    Friend WithEvents grpEmpInfo As System.Windows.Forms.GroupBox
    Friend WithEvents txtDateHired As System.Windows.Forms.Label
    Friend WithEvents lbDateHired As System.Windows.Forms.Label
    Friend WithEvents txtIdNumber As System.Windows.Forms.Label
    Friend WithEvents lblIdNumber As System.Windows.Forms.Label
    Friend WithEvents txtEmpStatus As System.Windows.Forms.Label
    Friend WithEvents lblEmpStatus As System.Windows.Forms.Label
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents txtPosition As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.Label
    Friend WithEvents lblPosition As System.Windows.Forms.Label
    Friend WithEvents txtDepartment As System.Windows.Forms.Label
    Friend WithEvents lblDepartment As System.Windows.Forms.Label
    Friend WithEvents gprLeaveInfo As System.Windows.Forms.GroupBox
    Friend WithEvents lblManagerStatus As System.Windows.Forms.Label
    Friend WithEvents cmbManagerStatus As System.Windows.Forms.ComboBox
    Friend WithEvents lblManager As System.Windows.Forms.Label
    Friend WithEvents cmbManagerName As System.Windows.Forms.ComboBox
    Friend WithEvents lblManagerName As System.Windows.Forms.Label
    Friend WithEvents lblManagerPosition As System.Windows.Forms.Label
    Friend WithEvents lblManagerDateApproved As System.Windows.Forms.Label
    Friend WithEvents lblManagerRemarks As System.Windows.Forms.Label
    Friend WithEvents txtManagerDateApproved As System.Windows.Forms.Label
    Friend WithEvents txtManagerRemarks As System.Windows.Forms.TextBox
    Friend WithEvents txtManagerPosition As System.Windows.Forms.Label
    Friend WithEvents lblSuperiorStatus As System.Windows.Forms.Label
    Friend WithEvents cmbSuperiorStatus As System.Windows.Forms.ComboBox
    Friend WithEvents lblSuperior As System.Windows.Forms.Label
    Friend WithEvents cmbSuperiorName As System.Windows.Forms.ComboBox
    Friend WithEvents lblSuperiorName As System.Windows.Forms.Label
    Friend WithEvents lblSuperiorPosition As System.Windows.Forms.Label
    Friend WithEvents lblSuperiorDateApproved As System.Windows.Forms.Label
    Friend WithEvents lblSuperiorRemarks As System.Windows.Forms.Label
    Friend WithEvents txtSuperiorDateApproved As System.Windows.Forms.Label
    Friend WithEvents txtSuperiorRemarks As System.Windows.Forms.TextBox
    Friend WithEvents txtSuperiorPosition As System.Windows.Forms.Label
    Friend WithEvents lblClinicStatus As System.Windows.Forms.Label
    Friend WithEvents cmbClinicStatus As System.Windows.Forms.ComboBox
    Friend WithEvents lblClinic As System.Windows.Forms.Label
    Friend WithEvents cmbClinicName As System.Windows.Forms.ComboBox
    Friend WithEvents lblClinicName As System.Windows.Forms.Label
    Friend WithEvents lblClinicPosition As System.Windows.Forms.Label
    Friend WithEvents lblClinicDateApproved As System.Windows.Forms.Label
    Friend WithEvents lblClinicRemarks As System.Windows.Forms.Label
    Friend WithEvents txtClinicDateApproved As System.Windows.Forms.Label
    Friend WithEvents txtClinicRemarks As System.Windows.Forms.TextBox
    Friend WithEvents txtClinicPosition As System.Windows.Forms.Label
    Friend WithEvents lblApprovers As System.Windows.Forms.Label
End Class
