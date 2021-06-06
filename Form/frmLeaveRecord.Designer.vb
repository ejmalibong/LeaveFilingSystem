<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLeaveRecord
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLeaveRecord))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.btnClose = New PinkieControls.ButtonXP()
        Me.btnView = New PinkieControls.ButtonXP()
        Me.btnApplyLeave = New PinkieControls.ButtonXP()
        Me.grpCriteria = New System.Windows.Forms.GroupBox()
        Me.pnlAbsentEndDate = New System.Windows.Forms.Panel()
        Me.dtpAbsentEndTo = New System.Windows.Forms.DateTimePicker()
        Me.lblAbsentEndTo = New System.Windows.Forms.Label()
        Me.lblAbsentEndFrom = New System.Windows.Forms.Label()
        Me.dtpAbsentEndFrom = New System.Windows.Forms.DateTimePicker()
        Me.pnlAbsentStartDate = New System.Windows.Forms.Panel()
        Me.dtpAbsentStartTo = New System.Windows.Forms.DateTimePicker()
        Me.lblAbsentStartTo = New System.Windows.Forms.Label()
        Me.lblAbsentStartFrom = New System.Windows.Forms.Label()
        Me.dtpAbsentStartFrom = New System.Windows.Forms.DateTimePicker()
        Me.pnlReason = New System.Windows.Forms.Panel()
        Me.txtReason = New System.Windows.Forms.TextBox()
        Me.pnlSection = New System.Windows.Forms.Panel()
        Me.cmbSection = New System.Windows.Forms.ComboBox()
        Me.pnlEmployeeName = New System.Windows.Forms.Panel()
        Me.txtEmployeeName = New System.Windows.Forms.TextBox()
        Me.pnlDateCreated = New System.Windows.Forms.Panel()
        Me.dtpDateCreatedTo = New System.Windows.Forms.DateTimePicker()
        Me.lblDateCreatedTo = New System.Windows.Forms.Label()
        Me.lblDateCreatedFrom = New System.Windows.Forms.Label()
        Me.dtpDateCreatedFrom = New System.Windows.Forms.DateTimePicker()
        Me.pnlLeaveType = New System.Windows.Forms.Panel()
        Me.cmbLeaveType = New System.Windows.Forms.ComboBox()
        Me.btnReset = New PinkieControls.ButtonXP()
        Me.lblSearchCriteria = New System.Windows.Forms.Label()
        Me.cmbSearchCriteria = New System.Windows.Forms.ComboBox()
        Me.bindingNavigator = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.txtTotalPageNumber = New System.Windows.Forms.ToolStripLabel()
        Me.BindingNavigatorMoveFirstItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMovePreviousItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.txtPageNumber = New System.Windows.Forms.ToolStripTextBox()
        Me.BindingNavigatorSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorMoveNextItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMoveLastItem = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.btnGo = New System.Windows.Forms.ToolStripButton()
        Me.dgvList = New System.Windows.Forms.DataGridView()
        Me.ColLeaveFileId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColDateCreated = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColLeaveTypeId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColLeaveTypeName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColEmployeeName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColStartDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColEndDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColQuantity = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColReason = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RoutingStatusId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColRoutingStatusName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.grpStatus = New System.Windows.Forms.GroupBox()
        Me.rdDisapproved = New System.Windows.Forms.RadioButton()
        Me.rdMyFile = New System.Windows.Forms.RadioButton()
        Me.rdPending = New System.Windows.Forms.RadioButton()
        Me.rdApproved = New System.Windows.Forms.RadioButton()
        Me.btnRefresh = New PinkieControls.ButtonXP()
        Me.btnDisapprove = New PinkieControls.ButtonXP()
        Me.btnApprove = New PinkieControls.ButtonXP()
        Me.btnSearch = New PinkieControls.ButtonXP()
        Me.grpCriteria.SuspendLayout()
        Me.pnlAbsentEndDate.SuspendLayout()
        Me.pnlAbsentStartDate.SuspendLayout()
        Me.pnlReason.SuspendLayout()
        Me.pnlSection.SuspendLayout()
        Me.pnlEmployeeName.SuspendLayout()
        Me.pnlDateCreated.SuspendLayout()
        Me.pnlLeaveType.SuspendLayout()
        CType(Me.bindingNavigator, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.bindingNavigator.SuspendLayout()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpStatus.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnClose.DefaultScheme = False
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Verdana", 10.5!)
        Me.btnClose.Hint = "Close leave record"
        Me.btnClose.Location = New System.Drawing.Point(1320, 549)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Scheme = PinkieControls.ButtonXP.Schemes.Blue
        Me.btnClose.Size = New System.Drawing.Size(110, 40)
        Me.btnClose.TabIndex = 155
        Me.btnClose.Text = "Close"
        '
        'btnView
        '
        Me.btnView.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnView.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnView.DefaultScheme = False
        Me.btnView.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnView.Font = New System.Drawing.Font("Verdana", 10.5!)
        Me.btnView.Hint = "View or modify record"
        Me.btnView.Image = Global.LeaveFilingSystem.My.Resources.Resources.Modify_16_x_16
        Me.btnView.Location = New System.Drawing.Point(1134, 549)
        Me.btnView.Name = "btnView"
        Me.btnView.Scheme = PinkieControls.ButtonXP.Schemes.Blue
        Me.btnView.Size = New System.Drawing.Size(180, 40)
        Me.btnView.TabIndex = 153
        Me.btnView.Text = "View / Edit Details"
        '
        'btnApplyLeave
        '
        Me.btnApplyLeave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnApplyLeave.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnApplyLeave.DefaultScheme = False
        Me.btnApplyLeave.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnApplyLeave.Font = New System.Drawing.Font("Verdana", 10.5!)
        Me.btnApplyLeave.Hint = "File new leave"
        Me.btnApplyLeave.Image = Global.LeaveFilingSystem.My.Resources.Resources.Create_16_x_16
        Me.btnApplyLeave.Location = New System.Drawing.Point(998, 549)
        Me.btnApplyLeave.Name = "btnApplyLeave"
        Me.btnApplyLeave.Scheme = PinkieControls.ButtonXP.Schemes.Blue
        Me.btnApplyLeave.Size = New System.Drawing.Size(130, 40)
        Me.btnApplyLeave.TabIndex = 152
        Me.btnApplyLeave.Text = "Apply Leave"
        '
        'grpCriteria
        '
        Me.grpCriteria.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpCriteria.Controls.Add(Me.pnlAbsentEndDate)
        Me.grpCriteria.Controls.Add(Me.pnlAbsentStartDate)
        Me.grpCriteria.Controls.Add(Me.pnlReason)
        Me.grpCriteria.Controls.Add(Me.pnlSection)
        Me.grpCriteria.Controls.Add(Me.pnlEmployeeName)
        Me.grpCriteria.Controls.Add(Me.pnlDateCreated)
        Me.grpCriteria.Controls.Add(Me.pnlLeaveType)
        Me.grpCriteria.Controls.Add(Me.btnReset)
        Me.grpCriteria.Controls.Add(Me.lblSearchCriteria)
        Me.grpCriteria.Controls.Add(Me.cmbSearchCriteria)
        Me.grpCriteria.Controls.Add(Me.bindingNavigator)
        Me.grpCriteria.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.grpCriteria.Location = New System.Drawing.Point(1, -6)
        Me.grpCriteria.Margin = New System.Windows.Forms.Padding(0)
        Me.grpCriteria.Name = "grpCriteria"
        Me.grpCriteria.Size = New System.Drawing.Size(1440, 44)
        Me.grpCriteria.TabIndex = 157
        Me.grpCriteria.TabStop = False
        '
        'pnlAbsentEndDate
        '
        Me.pnlAbsentEndDate.BackColor = System.Drawing.Color.White
        Me.pnlAbsentEndDate.Controls.Add(Me.dtpAbsentEndTo)
        Me.pnlAbsentEndDate.Controls.Add(Me.lblAbsentEndTo)
        Me.pnlAbsentEndDate.Controls.Add(Me.lblAbsentEndFrom)
        Me.pnlAbsentEndDate.Controls.Add(Me.dtpAbsentEndFrom)
        Me.pnlAbsentEndDate.Location = New System.Drawing.Point(257, 9)
        Me.pnlAbsentEndDate.Name = "pnlAbsentEndDate"
        Me.pnlAbsentEndDate.Size = New System.Drawing.Size(348, 32)
        Me.pnlAbsentEndDate.TabIndex = 533
        Me.pnlAbsentEndDate.Visible = False
        '
        'dtpAbsentEndTo
        '
        Me.dtpAbsentEndTo.CustomFormat = "MMM dd, yyyy"
        Me.dtpAbsentEndTo.Font = New System.Drawing.Font("Verdana", 9.5!)
        Me.dtpAbsentEndTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpAbsentEndTo.Location = New System.Drawing.Point(213, 5)
        Me.dtpAbsentEndTo.Name = "dtpAbsentEndTo"
        Me.dtpAbsentEndTo.Size = New System.Drawing.Size(130, 23)
        Me.dtpAbsentEndTo.TabIndex = 21
        '
        'lblAbsentEndTo
        '
        Me.lblAbsentEndTo.AutoSize = True
        Me.lblAbsentEndTo.Font = New System.Drawing.Font("Verdana", 9.5!)
        Me.lblAbsentEndTo.Location = New System.Drawing.Point(186, 9)
        Me.lblAbsentEndTo.Name = "lblAbsentEndTo"
        Me.lblAbsentEndTo.Size = New System.Drawing.Size(24, 16)
        Me.lblAbsentEndTo.TabIndex = 25
        Me.lblAbsentEndTo.Text = "To"
        '
        'lblAbsentEndFrom
        '
        Me.lblAbsentEndFrom.AutoSize = True
        Me.lblAbsentEndFrom.Font = New System.Drawing.Font("Verdana", 9.5!)
        Me.lblAbsentEndFrom.Location = New System.Drawing.Point(6, 9)
        Me.lblAbsentEndFrom.Name = "lblAbsentEndFrom"
        Me.lblAbsentEndFrom.Size = New System.Drawing.Size(40, 16)
        Me.lblAbsentEndFrom.TabIndex = 24
        Me.lblAbsentEndFrom.Text = "From"
        '
        'dtpAbsentEndFrom
        '
        Me.dtpAbsentEndFrom.CustomFormat = "MMM dd, yyyy"
        Me.dtpAbsentEndFrom.Font = New System.Drawing.Font("Verdana", 9.5!)
        Me.dtpAbsentEndFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpAbsentEndFrom.Location = New System.Drawing.Point(50, 5)
        Me.dtpAbsentEndFrom.Name = "dtpAbsentEndFrom"
        Me.dtpAbsentEndFrom.Size = New System.Drawing.Size(130, 23)
        Me.dtpAbsentEndFrom.TabIndex = 20
        '
        'pnlAbsentStartDate
        '
        Me.pnlAbsentStartDate.BackColor = System.Drawing.Color.White
        Me.pnlAbsentStartDate.Controls.Add(Me.dtpAbsentStartTo)
        Me.pnlAbsentStartDate.Controls.Add(Me.lblAbsentStartTo)
        Me.pnlAbsentStartDate.Controls.Add(Me.lblAbsentStartFrom)
        Me.pnlAbsentStartDate.Controls.Add(Me.dtpAbsentStartFrom)
        Me.pnlAbsentStartDate.Location = New System.Drawing.Point(257, 9)
        Me.pnlAbsentStartDate.Name = "pnlAbsentStartDate"
        Me.pnlAbsentStartDate.Size = New System.Drawing.Size(348, 32)
        Me.pnlAbsentStartDate.TabIndex = 533
        Me.pnlAbsentStartDate.Visible = False
        '
        'dtpAbsentStartTo
        '
        Me.dtpAbsentStartTo.CustomFormat = "MMM dd, yyyy"
        Me.dtpAbsentStartTo.Font = New System.Drawing.Font("Verdana", 9.5!)
        Me.dtpAbsentStartTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpAbsentStartTo.Location = New System.Drawing.Point(213, 5)
        Me.dtpAbsentStartTo.Name = "dtpAbsentStartTo"
        Me.dtpAbsentStartTo.Size = New System.Drawing.Size(130, 23)
        Me.dtpAbsentStartTo.TabIndex = 21
        '
        'lblAbsentStartTo
        '
        Me.lblAbsentStartTo.AutoSize = True
        Me.lblAbsentStartTo.Font = New System.Drawing.Font("Verdana", 9.5!)
        Me.lblAbsentStartTo.Location = New System.Drawing.Point(186, 9)
        Me.lblAbsentStartTo.Name = "lblAbsentStartTo"
        Me.lblAbsentStartTo.Size = New System.Drawing.Size(24, 16)
        Me.lblAbsentStartTo.TabIndex = 25
        Me.lblAbsentStartTo.Text = "To"
        '
        'lblAbsentStartFrom
        '
        Me.lblAbsentStartFrom.AutoSize = True
        Me.lblAbsentStartFrom.Font = New System.Drawing.Font("Verdana", 9.5!)
        Me.lblAbsentStartFrom.Location = New System.Drawing.Point(6, 9)
        Me.lblAbsentStartFrom.Name = "lblAbsentStartFrom"
        Me.lblAbsentStartFrom.Size = New System.Drawing.Size(40, 16)
        Me.lblAbsentStartFrom.TabIndex = 24
        Me.lblAbsentStartFrom.Text = "From"
        '
        'dtpAbsentStartFrom
        '
        Me.dtpAbsentStartFrom.CustomFormat = "MMM dd, yyyy"
        Me.dtpAbsentStartFrom.Font = New System.Drawing.Font("Verdana", 9.5!)
        Me.dtpAbsentStartFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpAbsentStartFrom.Location = New System.Drawing.Point(50, 5)
        Me.dtpAbsentStartFrom.Name = "dtpAbsentStartFrom"
        Me.dtpAbsentStartFrom.Size = New System.Drawing.Size(130, 23)
        Me.dtpAbsentStartFrom.TabIndex = 20
        '
        'pnlReason
        '
        Me.pnlReason.BackColor = System.Drawing.Color.White
        Me.pnlReason.Controls.Add(Me.txtReason)
        Me.pnlReason.Font = New System.Drawing.Font("Verdana", 9.5!)
        Me.pnlReason.Location = New System.Drawing.Point(257, 9)
        Me.pnlReason.Name = "pnlReason"
        Me.pnlReason.Size = New System.Drawing.Size(348, 32)
        Me.pnlReason.TabIndex = 531
        Me.pnlReason.Visible = False
        '
        'txtReason
        '
        Me.txtReason.Font = New System.Drawing.Font("Verdana", 9.5!)
        Me.txtReason.Location = New System.Drawing.Point(8, 5)
        Me.txtReason.Name = "txtReason"
        Me.txtReason.Size = New System.Drawing.Size(333, 23)
        Me.txtReason.TabIndex = 0
        '
        'pnlSection
        '
        Me.pnlSection.BackColor = System.Drawing.Color.White
        Me.pnlSection.Controls.Add(Me.cmbSection)
        Me.pnlSection.Font = New System.Drawing.Font("Verdana", 9.5!)
        Me.pnlSection.Location = New System.Drawing.Point(257, 9)
        Me.pnlSection.Name = "pnlSection"
        Me.pnlSection.Size = New System.Drawing.Size(348, 32)
        Me.pnlSection.TabIndex = 532
        Me.pnlSection.Visible = False
        '
        'cmbSection
        '
        Me.cmbSection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSection.Font = New System.Drawing.Font("Verdana", 9.5!)
        Me.cmbSection.FormattingEnabled = True
        Me.cmbSection.Location = New System.Drawing.Point(8, 4)
        Me.cmbSection.Name = "cmbSection"
        Me.cmbSection.Size = New System.Drawing.Size(333, 24)
        Me.cmbSection.TabIndex = 0
        '
        'pnlEmployeeName
        '
        Me.pnlEmployeeName.BackColor = System.Drawing.Color.White
        Me.pnlEmployeeName.Controls.Add(Me.txtEmployeeName)
        Me.pnlEmployeeName.Location = New System.Drawing.Point(257, 9)
        Me.pnlEmployeeName.Name = "pnlEmployeeName"
        Me.pnlEmployeeName.Size = New System.Drawing.Size(348, 32)
        Me.pnlEmployeeName.TabIndex = 530
        Me.pnlEmployeeName.Visible = False
        '
        'txtEmployeeName
        '
        Me.txtEmployeeName.Font = New System.Drawing.Font("Verdana", 9.5!)
        Me.txtEmployeeName.Location = New System.Drawing.Point(8, 4)
        Me.txtEmployeeName.Name = "txtEmployeeName"
        Me.txtEmployeeName.Size = New System.Drawing.Size(333, 23)
        Me.txtEmployeeName.TabIndex = 0
        '
        'pnlDateCreated
        '
        Me.pnlDateCreated.BackColor = System.Drawing.Color.White
        Me.pnlDateCreated.Controls.Add(Me.dtpDateCreatedTo)
        Me.pnlDateCreated.Controls.Add(Me.lblDateCreatedTo)
        Me.pnlDateCreated.Controls.Add(Me.lblDateCreatedFrom)
        Me.pnlDateCreated.Controls.Add(Me.dtpDateCreatedFrom)
        Me.pnlDateCreated.Location = New System.Drawing.Point(257, 9)
        Me.pnlDateCreated.Name = "pnlDateCreated"
        Me.pnlDateCreated.Size = New System.Drawing.Size(348, 32)
        Me.pnlDateCreated.TabIndex = 532
        Me.pnlDateCreated.Visible = False
        '
        'dtpDateCreatedTo
        '
        Me.dtpDateCreatedTo.CustomFormat = "MMM dd, yyyy"
        Me.dtpDateCreatedTo.Font = New System.Drawing.Font("Verdana", 9.5!)
        Me.dtpDateCreatedTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDateCreatedTo.Location = New System.Drawing.Point(213, 5)
        Me.dtpDateCreatedTo.Name = "dtpDateCreatedTo"
        Me.dtpDateCreatedTo.Size = New System.Drawing.Size(130, 23)
        Me.dtpDateCreatedTo.TabIndex = 21
        '
        'lblDateCreatedTo
        '
        Me.lblDateCreatedTo.AutoSize = True
        Me.lblDateCreatedTo.Font = New System.Drawing.Font("Verdana", 9.5!)
        Me.lblDateCreatedTo.Location = New System.Drawing.Point(186, 9)
        Me.lblDateCreatedTo.Name = "lblDateCreatedTo"
        Me.lblDateCreatedTo.Size = New System.Drawing.Size(24, 16)
        Me.lblDateCreatedTo.TabIndex = 25
        Me.lblDateCreatedTo.Text = "To"
        '
        'lblDateCreatedFrom
        '
        Me.lblDateCreatedFrom.AutoSize = True
        Me.lblDateCreatedFrom.Font = New System.Drawing.Font("Verdana", 9.5!)
        Me.lblDateCreatedFrom.Location = New System.Drawing.Point(6, 9)
        Me.lblDateCreatedFrom.Name = "lblDateCreatedFrom"
        Me.lblDateCreatedFrom.Size = New System.Drawing.Size(40, 16)
        Me.lblDateCreatedFrom.TabIndex = 24
        Me.lblDateCreatedFrom.Text = "From"
        '
        'dtpDateCreatedFrom
        '
        Me.dtpDateCreatedFrom.CustomFormat = "MMM dd, yyyy"
        Me.dtpDateCreatedFrom.Font = New System.Drawing.Font("Verdana", 9.5!)
        Me.dtpDateCreatedFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDateCreatedFrom.Location = New System.Drawing.Point(50, 5)
        Me.dtpDateCreatedFrom.Name = "dtpDateCreatedFrom"
        Me.dtpDateCreatedFrom.Size = New System.Drawing.Size(130, 23)
        Me.dtpDateCreatedFrom.TabIndex = 20
        '
        'pnlLeaveType
        '
        Me.pnlLeaveType.BackColor = System.Drawing.Color.White
        Me.pnlLeaveType.Controls.Add(Me.cmbLeaveType)
        Me.pnlLeaveType.Location = New System.Drawing.Point(257, 9)
        Me.pnlLeaveType.Name = "pnlLeaveType"
        Me.pnlLeaveType.Size = New System.Drawing.Size(348, 32)
        Me.pnlLeaveType.TabIndex = 531
        '
        'cmbLeaveType
        '
        Me.cmbLeaveType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLeaveType.Font = New System.Drawing.Font("Verdana", 9.5!)
        Me.cmbLeaveType.FormattingEnabled = True
        Me.cmbLeaveType.Location = New System.Drawing.Point(8, 4)
        Me.cmbLeaveType.Name = "cmbLeaveType"
        Me.cmbLeaveType.Size = New System.Drawing.Size(333, 24)
        Me.cmbLeaveType.TabIndex = 0
        '
        'btnReset
        '
        Me.btnReset.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.btnReset.DefaultScheme = False
        Me.btnReset.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnReset.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.btnReset.Hint = "Remove search filter"
        Me.btnReset.Image = Global.LeaveFilingSystem.My.Resources.Resources.Undo_16_x_16
        Me.btnReset.Location = New System.Drawing.Point(693, 11)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Scheme = PinkieControls.ButtonXP.Schemes.Blue
        Me.btnReset.Size = New System.Drawing.Size(85, 27)
        Me.btnReset.TabIndex = 535
        Me.btnReset.TabStop = False
        Me.btnReset.Text = "Reset"
        '
        'lblSearchCriteria
        '
        Me.lblSearchCriteria.BackColor = System.Drawing.SystemColors.Control
        Me.lblSearchCriteria.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSearchCriteria.ForeColor = System.Drawing.Color.Black
        Me.lblSearchCriteria.Location = New System.Drawing.Point(6, 13)
        Me.lblSearchCriteria.Name = "lblSearchCriteria"
        Me.lblSearchCriteria.Size = New System.Drawing.Size(65, 24)
        Me.lblSearchCriteria.TabIndex = 529
        Me.lblSearchCriteria.Text = "Criteria"
        Me.lblSearchCriteria.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmbSearchCriteria
        '
        Me.cmbSearchCriteria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSearchCriteria.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSearchCriteria.FormattingEnabled = True
        Me.cmbSearchCriteria.Location = New System.Drawing.Point(70, 13)
        Me.cmbSearchCriteria.Name = "cmbSearchCriteria"
        Me.cmbSearchCriteria.Size = New System.Drawing.Size(185, 24)
        Me.cmbSearchCriteria.TabIndex = 530
        '
        'bindingNavigator
        '
        Me.bindingNavigator.AddNewItem = Nothing
        Me.bindingNavigator.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.bindingNavigator.BackColor = System.Drawing.Color.White
        Me.bindingNavigator.CountItem = Me.txtTotalPageNumber
        Me.bindingNavigator.CountItemFormat = "of "
        Me.bindingNavigator.DeleteItem = Nothing
        Me.bindingNavigator.Dock = System.Windows.Forms.DockStyle.None
        Me.bindingNavigator.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.bindingNavigator.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.bindingNavigator.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BindingNavigatorMoveFirstItem, Me.BindingNavigatorMovePreviousItem, Me.BindingNavigatorSeparator, Me.txtPageNumber, Me.txtTotalPageNumber, Me.BindingNavigatorSeparator1, Me.BindingNavigatorMoveNextItem, Me.BindingNavigatorMoveLastItem, Me.ToolStripSeparator, Me.btnGo})
        Me.bindingNavigator.Location = New System.Drawing.Point(1237, 12)
        Me.bindingNavigator.MoveFirstItem = Me.BindingNavigatorMoveFirstItem
        Me.bindingNavigator.MoveLastItem = Me.BindingNavigatorMoveLastItem
        Me.bindingNavigator.MoveNextItem = Me.BindingNavigatorMoveNextItem
        Me.bindingNavigator.MovePreviousItem = Me.BindingNavigatorMovePreviousItem
        Me.bindingNavigator.Name = "bindingNavigator"
        Me.bindingNavigator.PositionItem = Me.txtPageNumber
        Me.bindingNavigator.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.bindingNavigator.Size = New System.Drawing.Size(201, 25)
        Me.bindingNavigator.TabIndex = 165
        Me.bindingNavigator.Text = "PagerPanel"
        '
        'txtTotalPageNumber
        '
        Me.txtTotalPageNumber.Name = "txtTotalPageNumber"
        Me.txtTotalPageNumber.Size = New System.Drawing.Size(21, 22)
        Me.txtTotalPageNumber.Text = "of "
        Me.txtTotalPageNumber.ToolTipText = "Total number of items"
        '
        'BindingNavigatorMoveFirstItem
        '
        Me.BindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveFirstItem.Image = CType(resources.GetObject("BindingNavigatorMoveFirstItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveFirstItem.Name = "BindingNavigatorMoveFirstItem"
        Me.BindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveFirstItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveFirstItem.Text = "Move first"
        '
        'BindingNavigatorMovePreviousItem
        '
        Me.BindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMovePreviousItem.Image = CType(resources.GetObject("BindingNavigatorMovePreviousItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMovePreviousItem.Name = "BindingNavigatorMovePreviousItem"
        Me.BindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMovePreviousItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMovePreviousItem.Text = "Move previous"
        '
        'BindingNavigatorSeparator
        '
        Me.BindingNavigatorSeparator.Name = "BindingNavigatorSeparator"
        Me.BindingNavigatorSeparator.Size = New System.Drawing.Size(6, 25)
        '
        'txtPageNumber
        '
        Me.txtPageNumber.AccessibleName = "Position"
        Me.txtPageNumber.AutoSize = False
        Me.txtPageNumber.Name = "txtPageNumber"
        Me.txtPageNumber.Size = New System.Drawing.Size(30, 23)
        Me.txtPageNumber.Text = "0"
        Me.txtPageNumber.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtPageNumber.ToolTipText = "Current page"
        '
        'BindingNavigatorSeparator1
        '
        Me.BindingNavigatorSeparator1.Name = "BindingNavigatorSeparator1"
        Me.BindingNavigatorSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'BindingNavigatorMoveNextItem
        '
        Me.BindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveNextItem.Image = CType(resources.GetObject("BindingNavigatorMoveNextItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveNextItem.Name = "BindingNavigatorMoveNextItem"
        Me.BindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveNextItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveNextItem.Text = "Move next"
        '
        'BindingNavigatorMoveLastItem
        '
        Me.BindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveLastItem.Image = CType(resources.GetObject("BindingNavigatorMoveLastItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveLastItem.Name = "BindingNavigatorMoveLastItem"
        Me.BindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveLastItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveLastItem.Text = "Move last"
        '
        'ToolStripSeparator
        '
        Me.ToolStripSeparator.Name = "ToolStripSeparator"
        Me.ToolStripSeparator.Size = New System.Drawing.Size(6, 25)
        '
        'btnGo
        '
        Me.btnGo.AutoSize = False
        Me.btnGo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnGo.Image = CType(resources.GetObject("btnGo.Image"), System.Drawing.Image)
        Me.btnGo.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(35, 22)
        Me.btnGo.Text = "Go"
        Me.btnGo.ToolTipText = "Go to page number specified"
        '
        'dgvList
        '
        Me.dgvList.AllowUserToAddRows = False
        Me.dgvList.AllowUserToDeleteRows = False
        Me.dgvList.AllowUserToResizeRows = False
        Me.dgvList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.dgvList.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvList.ColumnHeadersHeight = 26
        Me.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColLeaveFileId, Me.ColDateCreated, Me.ColLeaveTypeId, Me.ColLeaveTypeName, Me.ColEmployeeName, Me.ColStartDate, Me.ColEndDate, Me.ColQuantity, Me.ColReason, Me.RoutingStatusId, Me.ColRoutingStatusName})
        Me.dgvList.Location = New System.Drawing.Point(0, 38)
        Me.dgvList.MultiSelect = False
        Me.dgvList.Name = "dgvList"
        Me.dgvList.ReadOnly = True
        Me.dgvList.RowHeadersVisible = False
        Me.dgvList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvList.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvList.Size = New System.Drawing.Size(1442, 500)
        Me.dgvList.TabIndex = 158
        '
        'ColLeaveFileId
        '
        Me.ColLeaveFileId.DataPropertyName = "LeaveFileId"
        Me.ColLeaveFileId.HeaderText = "LeaveFilingId"
        Me.ColLeaveFileId.Name = "ColLeaveFileId"
        Me.ColLeaveFileId.ReadOnly = True
        Me.ColLeaveFileId.Visible = False
        '
        'ColDateCreated
        '
        Me.ColDateCreated.DataPropertyName = "DateCreated"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.ColDateCreated.DefaultCellStyle = DataGridViewCellStyle2
        Me.ColDateCreated.HeaderText = "Date Created"
        Me.ColDateCreated.Name = "ColDateCreated"
        Me.ColDateCreated.ReadOnly = True
        Me.ColDateCreated.Width = 150
        '
        'ColLeaveTypeId
        '
        Me.ColLeaveTypeId.DataPropertyName = "LeaveTypeId"
        Me.ColLeaveTypeId.HeaderText = "LeaveTypeId"
        Me.ColLeaveTypeId.Name = "ColLeaveTypeId"
        Me.ColLeaveTypeId.ReadOnly = True
        Me.ColLeaveTypeId.Visible = False
        '
        'ColLeaveTypeName
        '
        Me.ColLeaveTypeName.DataPropertyName = "LeaveTypeName"
        Me.ColLeaveTypeName.HeaderText = "Leave Type"
        Me.ColLeaveTypeName.Name = "ColLeaveTypeName"
        Me.ColLeaveTypeName.ReadOnly = True
        Me.ColLeaveTypeName.Width = 130
        '
        'ColEmployeeName
        '
        Me.ColEmployeeName.DataPropertyName = "EmployeeName"
        Me.ColEmployeeName.HeaderText = "Name"
        Me.ColEmployeeName.Name = "ColEmployeeName"
        Me.ColEmployeeName.ReadOnly = True
        Me.ColEmployeeName.Width = 270
        '
        'ColStartDate
        '
        Me.ColStartDate.DataPropertyName = "StartDate"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.ColStartDate.DefaultCellStyle = DataGridViewCellStyle3
        Me.ColStartDate.HeaderText = "From"
        Me.ColStartDate.Name = "ColStartDate"
        Me.ColStartDate.ReadOnly = True
        '
        'ColEndDate
        '
        Me.ColEndDate.DataPropertyName = "EndDate"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.ColEndDate.DefaultCellStyle = DataGridViewCellStyle4
        Me.ColEndDate.HeaderText = "To"
        Me.ColEndDate.Name = "ColEndDate"
        Me.ColEndDate.ReadOnly = True
        '
        'ColQuantity
        '
        Me.ColQuantity.DataPropertyName = "Quantity"
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.ColQuantity.DefaultCellStyle = DataGridViewCellStyle5
        Me.ColQuantity.HeaderText = "Qty"
        Me.ColQuantity.Name = "ColQuantity"
        Me.ColQuantity.ReadOnly = True
        Me.ColQuantity.Width = 55
        '
        'ColReason
        '
        Me.ColReason.DataPropertyName = "Reason"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.ColReason.DefaultCellStyle = DataGridViewCellStyle6
        Me.ColReason.HeaderText = "Reason"
        Me.ColReason.Name = "ColReason"
        Me.ColReason.ReadOnly = True
        Me.ColReason.Width = 300
        '
        'RoutingStatusId
        '
        Me.RoutingStatusId.DataPropertyName = "RoutingStatusId"
        Me.RoutingStatusId.HeaderText = "RoutingStatusId"
        Me.RoutingStatusId.Name = "RoutingStatusId"
        Me.RoutingStatusId.ReadOnly = True
        Me.RoutingStatusId.Visible = False
        '
        'ColRoutingStatusName
        '
        Me.ColRoutingStatusName.DataPropertyName = "RoutingStatusName"
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.ColRoutingStatusName.DefaultCellStyle = DataGridViewCellStyle7
        Me.ColRoutingStatusName.HeaderText = "Status"
        Me.ColRoutingStatusName.Name = "ColRoutingStatusName"
        Me.ColRoutingStatusName.ReadOnly = True
        Me.ColRoutingStatusName.Width = 300
        '
        'grpStatus
        '
        Me.grpStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grpStatus.BackColor = System.Drawing.Color.White
        Me.grpStatus.Controls.Add(Me.rdDisapproved)
        Me.grpStatus.Controls.Add(Me.rdMyFile)
        Me.grpStatus.Controls.Add(Me.rdPending)
        Me.grpStatus.Controls.Add(Me.rdApproved)
        Me.grpStatus.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.grpStatus.Location = New System.Drawing.Point(11, 541)
        Me.grpStatus.Margin = New System.Windows.Forms.Padding(0)
        Me.grpStatus.Name = "grpStatus"
        Me.grpStatus.Padding = New System.Windows.Forms.Padding(0)
        Me.grpStatus.Size = New System.Drawing.Size(451, 48)
        Me.grpStatus.TabIndex = 159
        Me.grpStatus.TabStop = False
        '
        'rdDisapproved
        '
        Me.rdDisapproved.AutoSize = True
        Me.rdDisapproved.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.rdDisapproved.Location = New System.Drawing.Point(9, 15)
        Me.rdDisapproved.Name = "rdDisapproved"
        Me.rdDisapproved.Size = New System.Drawing.Size(112, 24)
        Me.rdDisapproved.TabIndex = 4
        Me.rdDisapproved.TabStop = True
        Me.rdDisapproved.Text = "Disapproved"
        Me.rdDisapproved.UseVisualStyleBackColor = True
        '
        'rdMyFile
        '
        Me.rdMyFile.AutoSize = True
        Me.rdMyFile.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.rdMyFile.Location = New System.Drawing.Point(320, 15)
        Me.rdMyFile.Name = "rdMyFile"
        Me.rdMyFile.Size = New System.Drawing.Size(128, 24)
        Me.rdMyFile.TabIndex = 3
        Me.rdMyFile.TabStop = True
        Me.rdMyFile.Text = "My Application"
        Me.rdMyFile.UseVisualStyleBackColor = True
        '
        'rdPending
        '
        Me.rdPending.AutoSize = True
        Me.rdPending.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.rdPending.Location = New System.Drawing.Point(226, 15)
        Me.rdPending.Name = "rdPending"
        Me.rdPending.Size = New System.Drawing.Size(80, 24)
        Me.rdPending.TabIndex = 2
        Me.rdPending.TabStop = True
        Me.rdPending.Text = "Pending"
        Me.rdPending.UseVisualStyleBackColor = True
        '
        'rdApproved
        '
        Me.rdApproved.AutoSize = True
        Me.rdApproved.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.rdApproved.Location = New System.Drawing.Point(126, 15)
        Me.rdApproved.Name = "rdApproved"
        Me.rdApproved.Size = New System.Drawing.Size(93, 24)
        Me.rdApproved.TabIndex = 1
        Me.rdApproved.TabStop = True
        Me.rdApproved.Text = "Approved"
        Me.rdApproved.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnRefresh.DefaultScheme = False
        Me.btnRefresh.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnRefresh.Font = New System.Drawing.Font("Verdana", 10.5!)
        Me.btnRefresh.Hint = "Refresh list"
        Me.btnRefresh.Image = Global.LeaveFilingSystem.My.Resources.Resources.Refresh_16_x_16
        Me.btnRefresh.Location = New System.Drawing.Point(468, 549)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Scheme = PinkieControls.ButtonXP.Schemes.Blue
        Me.btnRefresh.Size = New System.Drawing.Size(120, 40)
        Me.btnRefresh.TabIndex = 160
        Me.btnRefresh.Text = " Refresh"
        '
        'btnDisapprove
        '
        Me.btnDisapprove.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDisapprove.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnDisapprove.DefaultScheme = False
        Me.btnDisapprove.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnDisapprove.Font = New System.Drawing.Font("Verdana", 10.5!)
        Me.btnDisapprove.Hint = "Disapproved selected record"
        Me.btnDisapprove.Image = Global.LeaveFilingSystem.My.Resources.Resources.Delete_16_x_16
        Me.btnDisapprove.Location = New System.Drawing.Point(720, 549)
        Me.btnDisapprove.Name = "btnDisapprove"
        Me.btnDisapprove.Scheme = PinkieControls.ButtonXP.Schemes.Blue
        Me.btnDisapprove.Size = New System.Drawing.Size(120, 40)
        Me.btnDisapprove.TabIndex = 162
        Me.btnDisapprove.Text = "Disapprove"
        '
        'btnApprove
        '
        Me.btnApprove.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnApprove.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnApprove.DefaultScheme = False
        Me.btnApprove.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnApprove.Font = New System.Drawing.Font("Verdana", 10.5!)
        Me.btnApprove.Hint = "Approve selected record"
        Me.btnApprove.Image = Global.LeaveFilingSystem.My.Resources.Resources.Apply_16_x_16
        Me.btnApprove.Location = New System.Drawing.Point(594, 549)
        Me.btnApprove.Name = "btnApprove"
        Me.btnApprove.Scheme = PinkieControls.ButtonXP.Schemes.Blue
        Me.btnApprove.Size = New System.Drawing.Size(120, 40)
        Me.btnApprove.TabIndex = 161
        Me.btnApprove.Text = " Approve"
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnSearch.DefaultScheme = False
        Me.btnSearch.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnSearch.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.btnSearch.Hint = "Search"
        Me.btnSearch.Image = Global.LeaveFilingSystem.My.Resources.Resources.Find_16_x_16
        Me.btnSearch.Location = New System.Drawing.Point(606, 5)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Scheme = PinkieControls.ButtonXP.Schemes.Blue
        Me.btnSearch.Size = New System.Drawing.Size(85, 27)
        Me.btnSearch.TabIndex = 534
        Me.btnSearch.TabStop = False
        Me.btnSearch.Text = "Search"
        '
        'frmLeaveRecord
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(1442, 600)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.btnDisapprove)
        Me.Controls.Add(Me.btnApprove)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.dgvList)
        Me.Controls.Add(Me.grpCriteria)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnView)
        Me.Controls.Add(Me.btnApplyLeave)
        Me.Controls.Add(Me.grpStatus)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.MinimumSize = New System.Drawing.Size(1300, 600)
        Me.Name = "frmLeaveRecord"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Leave Records"
        Me.grpCriteria.ResumeLayout(False)
        Me.grpCriteria.PerformLayout()
        Me.pnlAbsentEndDate.ResumeLayout(False)
        Me.pnlAbsentEndDate.PerformLayout()
        Me.pnlAbsentStartDate.ResumeLayout(False)
        Me.pnlAbsentStartDate.PerformLayout()
        Me.pnlReason.ResumeLayout(False)
        Me.pnlReason.PerformLayout()
        Me.pnlSection.ResumeLayout(False)
        Me.pnlEmployeeName.ResumeLayout(False)
        Me.pnlEmployeeName.PerformLayout()
        Me.pnlDateCreated.ResumeLayout(False)
        Me.pnlDateCreated.PerformLayout()
        Me.pnlLeaveType.ResumeLayout(False)
        CType(Me.bindingNavigator, System.ComponentModel.ISupportInitialize).EndInit()
        Me.bindingNavigator.ResumeLayout(False)
        Me.bindingNavigator.PerformLayout()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpStatus.ResumeLayout(False)
        Me.grpStatus.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnClose As PinkieControls.ButtonXP
    Friend WithEvents btnView As PinkieControls.ButtonXP
    Friend WithEvents btnApplyLeave As PinkieControls.ButtonXP
    Friend WithEvents grpCriteria As System.Windows.Forms.GroupBox
    Friend WithEvents dgvList As System.Windows.Forms.DataGridView
    Friend WithEvents grpStatus As System.Windows.Forms.GroupBox
    Friend WithEvents rdPending As System.Windows.Forms.RadioButton
    Friend WithEvents rdApproved As System.Windows.Forms.RadioButton
    Friend WithEvents btnRefresh As PinkieControls.ButtonXP
    Friend WithEvents rdMyFile As System.Windows.Forms.RadioButton
    Friend WithEvents btnDisapprove As PinkieControls.ButtonXP
    Friend WithEvents btnApprove As PinkieControls.ButtonXP
    Friend WithEvents bindingNavigator As System.Windows.Forms.BindingNavigator
    Friend WithEvents txtTotalPageNumber As System.Windows.Forms.ToolStripLabel
    Friend WithEvents BindingNavigatorMoveFirstItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMovePreviousItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents txtPageNumber As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents BindingNavigatorSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorMoveNextItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMoveLastItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnGo As System.Windows.Forms.ToolStripButton
    Friend WithEvents rdDisapproved As System.Windows.Forms.RadioButton
    Friend WithEvents ColLeaveFileId As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColDateCreated As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColLeaveTypeId As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColLeaveTypeName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColEmployeeName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColStartDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColEndDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColQuantity As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColReason As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RoutingStatusId As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColRoutingStatusName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lblSearchCriteria As System.Windows.Forms.Label
    Friend WithEvents cmbSearchCriteria As System.Windows.Forms.ComboBox
    Friend WithEvents pnlReason As System.Windows.Forms.Panel
    Friend WithEvents txtReason As System.Windows.Forms.TextBox
    Friend WithEvents pnlDateCreated As System.Windows.Forms.Panel
    Friend WithEvents dtpDateCreatedTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblDateCreatedTo As System.Windows.Forms.Label
    Friend WithEvents lblDateCreatedFrom As System.Windows.Forms.Label
    Friend WithEvents dtpDateCreatedFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents pnlSection As System.Windows.Forms.Panel
    Friend WithEvents cmbSection As System.Windows.Forms.ComboBox
    Friend WithEvents pnlEmployeeName As System.Windows.Forms.Panel
    Friend WithEvents txtEmployeeName As System.Windows.Forms.TextBox
    Friend WithEvents pnlLeaveType As System.Windows.Forms.Panel
    Friend WithEvents cmbLeaveType As System.Windows.Forms.ComboBox
    Friend WithEvents pnlAbsentEndDate As System.Windows.Forms.Panel
    Friend WithEvents dtpAbsentEndTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblAbsentEndTo As System.Windows.Forms.Label
    Friend WithEvents lblAbsentEndFrom As System.Windows.Forms.Label
    Friend WithEvents dtpAbsentEndFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents pnlAbsentStartDate As System.Windows.Forms.Panel
    Friend WithEvents dtpAbsentStartTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblAbsentStartTo As System.Windows.Forms.Label
    Friend WithEvents lblAbsentStartFrom As System.Windows.Forms.Label
    Friend WithEvents dtpAbsentStartFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnSearch As PinkieControls.ButtonXP
    Friend WithEvents btnReset As PinkieControls.ButtonXP
End Class
