<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLeaveList
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLeaveList))
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.btnClose = New PinkieControls.ButtonXP()
        Me.btnView = New PinkieControls.ButtonXP()
        Me.btnFileLeave = New PinkieControls.ButtonXP()
        Me.grpCriteria = New System.Windows.Forms.GroupBox()
        Me.bindingNavigator = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.txtTotalPageNumber = New System.Windows.Forms.ToolStripLabel()
        Me.BindingNavigatorMoveFirstItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMovePreviousItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.txtPageNumber = New System.Windows.Forms.ToolStripTextBox()
        Me.BindingNavigatorSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorMoveNextItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMoveLastItem = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnGo = New System.Windows.Forms.ToolStripButton()
        Me.pnlDateSearch = New System.Windows.Forms.Panel()
        Me.lblStartFrom = New System.Windows.Forms.Label()
        Me.btnResetDate = New System.Windows.Forms.Button()
        Me.btnSearchDate = New System.Windows.Forms.Button()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.lblStartTo = New System.Windows.Forms.Label()
        Me.lblCriteria = New System.Windows.Forms.Label()
        Me.cmbSearchCriteria = New System.Windows.Forms.ComboBox()
        Me.dgvList = New System.Windows.Forms.DataGridView()
        Me.grpStatus = New System.Windows.Forms.GroupBox()
        Me.rdMyFile = New System.Windows.Forms.RadioButton()
        Me.rdPending = New System.Windows.Forms.RadioButton()
        Me.rdApproved = New System.Windows.Forms.RadioButton()
        Me.btnRefresh = New PinkieControls.ButtonXP()
        Me.btnDisapprove = New PinkieControls.ButtonXP()
        Me.btnApprove = New PinkieControls.ButtonXP()
        Me.ColLeaveFilingId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColDateFiled = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColLeaveTypeId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColLeaveTypeName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColStartDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColEndDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColQuantity = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColReason = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColClinicIsApproved = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColClinicClearance = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RoutingStatusId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColRoutingStatusName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.grpCriteria.SuspendLayout()
        CType(Me.bindingNavigator, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.bindingNavigator.SuspendLayout()
        Me.pnlDateSearch.SuspendLayout()
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
        Me.btnClose.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.btnClose.Hint = ""
        Me.btnClose.Location = New System.Drawing.Point(1182, 561)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Scheme = PinkieControls.ButtonXP.Schemes.Blue
        Me.btnClose.Size = New System.Drawing.Size(120, 32)
        Me.btnClose.TabIndex = 155
        Me.btnClose.Text = "Close"
        '
        'btnView
        '
        Me.btnView.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnView.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnView.DefaultScheme = False
        Me.btnView.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnView.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.btnView.Hint = ""
        Me.btnView.Location = New System.Drawing.Point(1059, 561)
        Me.btnView.Name = "btnView"
        Me.btnView.Scheme = PinkieControls.ButtonXP.Schemes.Blue
        Me.btnView.Size = New System.Drawing.Size(120, 32)
        Me.btnView.TabIndex = 153
        Me.btnView.Text = "View Details"
        '
        'btnFileLeave
        '
        Me.btnFileLeave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnFileLeave.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnFileLeave.DefaultScheme = False
        Me.btnFileLeave.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnFileLeave.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.btnFileLeave.Hint = ""
        Me.btnFileLeave.Location = New System.Drawing.Point(936, 561)
        Me.btnFileLeave.Name = "btnFileLeave"
        Me.btnFileLeave.Scheme = PinkieControls.ButtonXP.Schemes.Blue
        Me.btnFileLeave.Size = New System.Drawing.Size(120, 32)
        Me.btnFileLeave.TabIndex = 152
        Me.btnFileLeave.Text = "Apply Leave"
        '
        'grpCriteria
        '
        Me.grpCriteria.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpCriteria.Controls.Add(Me.bindingNavigator)
        Me.grpCriteria.Controls.Add(Me.pnlDateSearch)
        Me.grpCriteria.Controls.Add(Me.lblCriteria)
        Me.grpCriteria.Controls.Add(Me.cmbSearchCriteria)
        Me.grpCriteria.Location = New System.Drawing.Point(1, -6)
        Me.grpCriteria.Margin = New System.Windows.Forms.Padding(0)
        Me.grpCriteria.Name = "grpCriteria"
        Me.grpCriteria.Size = New System.Drawing.Size(1312, 40)
        Me.grpCriteria.TabIndex = 157
        Me.grpCriteria.TabStop = False
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
        Me.bindingNavigator.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.bindingNavigator.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BindingNavigatorMoveFirstItem, Me.BindingNavigatorMovePreviousItem, Me.BindingNavigatorSeparator, Me.txtPageNumber, Me.txtTotalPageNumber, Me.BindingNavigatorSeparator1, Me.BindingNavigatorMoveNextItem, Me.BindingNavigatorMoveLastItem, Me.ToolStripSeparator1, Me.btnGo})
        Me.bindingNavigator.Location = New System.Drawing.Point(1112, 10)
        Me.bindingNavigator.MoveFirstItem = Me.BindingNavigatorMoveFirstItem
        Me.bindingNavigator.MoveLastItem = Me.BindingNavigatorMoveLastItem
        Me.bindingNavigator.MoveNextItem = Me.BindingNavigatorMoveNextItem
        Me.bindingNavigator.MovePreviousItem = Me.BindingNavigatorMovePreviousItem
        Me.bindingNavigator.Name = "bindingNavigator"
        Me.bindingNavigator.PositionItem = Me.txtPageNumber
        Me.bindingNavigator.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.bindingNavigator.Size = New System.Drawing.Size(201, 25)
        Me.bindingNavigator.TabIndex = 11
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
        Me.txtPageNumber.ToolTipText = "Current position"
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
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
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
        '
        'pnlDateSearch
        '
        Me.pnlDateSearch.Controls.Add(Me.lblStartFrom)
        Me.pnlDateSearch.Controls.Add(Me.btnResetDate)
        Me.pnlDateSearch.Controls.Add(Me.btnSearchDate)
        Me.pnlDateSearch.Controls.Add(Me.dtpFrom)
        Me.pnlDateSearch.Controls.Add(Me.dtpTo)
        Me.pnlDateSearch.Controls.Add(Me.lblStartTo)
        Me.pnlDateSearch.Location = New System.Drawing.Point(265, 9)
        Me.pnlDateSearch.Name = "pnlDateSearch"
        Me.pnlDateSearch.Size = New System.Drawing.Size(520, 28)
        Me.pnlDateSearch.TabIndex = 6
        '
        'lblStartFrom
        '
        Me.lblStartFrom.AutoSize = True
        Me.lblStartFrom.Location = New System.Drawing.Point(3, 7)
        Me.lblStartFrom.Name = "lblStartFrom"
        Me.lblStartFrom.Size = New System.Drawing.Size(38, 14)
        Me.lblStartFrom.TabIndex = 18
        Me.lblStartFrom.Text = "From"
        '
        'btnResetDate
        '
        Me.btnResetDate.Location = New System.Drawing.Point(433, 2)
        Me.btnResetDate.Name = "btnResetDate"
        Me.btnResetDate.Size = New System.Drawing.Size(85, 24)
        Me.btnResetDate.TabIndex = 5
        Me.btnResetDate.Text = "Reset"
        Me.btnResetDate.UseVisualStyleBackColor = True
        '
        'btnSearchDate
        '
        Me.btnSearchDate.Location = New System.Drawing.Point(346, 2)
        Me.btnSearchDate.Name = "btnSearchDate"
        Me.btnSearchDate.Size = New System.Drawing.Size(85, 24)
        Me.btnSearchDate.TabIndex = 4
        Me.btnSearchDate.Text = "Search"
        Me.btnSearchDate.UseVisualStyleBackColor = True
        '
        'dtpFrom
        '
        Me.dtpFrom.CustomFormat = "MMM dd, yyyy"
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(47, 3)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(130, 21)
        Me.dtpFrom.TabIndex = 2
        '
        'dtpTo
        '
        Me.dtpTo.CustomFormat = "MMM dd, yyyy"
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(210, 3)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(130, 21)
        Me.dtpTo.TabIndex = 3
        '
        'lblStartTo
        '
        Me.lblStartTo.AutoSize = True
        Me.lblStartTo.Location = New System.Drawing.Point(183, 7)
        Me.lblStartTo.Name = "lblStartTo"
        Me.lblStartTo.Size = New System.Drawing.Size(21, 14)
        Me.lblStartTo.TabIndex = 19
        Me.lblStartTo.Text = "To"
        '
        'lblCriteria
        '
        Me.lblCriteria.BackColor = System.Drawing.SystemColors.Control
        Me.lblCriteria.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCriteria.Font = New System.Drawing.Font("Verdana", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCriteria.ForeColor = System.Drawing.Color.Black
        Me.lblCriteria.Location = New System.Drawing.Point(6, 12)
        Me.lblCriteria.Name = "lblCriteria"
        Me.lblCriteria.Size = New System.Drawing.Size(62, 22)
        Me.lblCriteria.TabIndex = 7
        Me.lblCriteria.Text = " Criteria"
        Me.lblCriteria.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbSearchCriteria
        '
        Me.cmbSearchCriteria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSearchCriteria.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSearchCriteria.FormattingEnabled = True
        Me.cmbSearchCriteria.Location = New System.Drawing.Point(67, 12)
        Me.cmbSearchCriteria.Name = "cmbSearchCriteria"
        Me.cmbSearchCriteria.Size = New System.Drawing.Size(194, 22)
        Me.cmbSearchCriteria.TabIndex = 8
        '
        'dgvList
        '
        Me.dgvList.AllowUserToAddRows = False
        Me.dgvList.AllowUserToDeleteRows = False
        Me.dgvList.AllowUserToResizeColumns = False
        Me.dgvList.AllowUserToResizeRows = False
        Me.dgvList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Verdana", 8.5!)
        DataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.dgvList.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle11
        Me.dgvList.ColumnHeadersHeight = 25
        Me.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColLeaveFilingId, Me.ColDateFiled, Me.ColLeaveTypeId, Me.ColLeaveTypeName, Me.ColName, Me.ColStartDate, Me.ColEndDate, Me.ColQuantity, Me.ColReason, Me.ColClinicIsApproved, Me.ColClinicClearance, Me.RoutingStatusId, Me.ColRoutingStatusName})
        Me.dgvList.Location = New System.Drawing.Point(0, 37)
        Me.dgvList.MultiSelect = False
        Me.dgvList.Name = "dgvList"
        Me.dgvList.ReadOnly = True
        Me.dgvList.RowHeadersVisible = False
        Me.dgvList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvList.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvList.Size = New System.Drawing.Size(1314, 515)
        Me.dgvList.TabIndex = 158
        '
        'grpStatus
        '
        Me.grpStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grpStatus.BackColor = System.Drawing.Color.White
        Me.grpStatus.Controls.Add(Me.rdMyFile)
        Me.grpStatus.Controls.Add(Me.rdPending)
        Me.grpStatus.Controls.Add(Me.rdApproved)
        Me.grpStatus.Location = New System.Drawing.Point(7, 555)
        Me.grpStatus.Margin = New System.Windows.Forms.Padding(0)
        Me.grpStatus.Name = "grpStatus"
        Me.grpStatus.Padding = New System.Windows.Forms.Padding(0)
        Me.grpStatus.Size = New System.Drawing.Size(319, 38)
        Me.grpStatus.TabIndex = 159
        Me.grpStatus.TabStop = False
        '
        'rdMyFile
        '
        Me.rdMyFile.AutoSize = True
        Me.rdMyFile.Location = New System.Drawing.Point(200, 12)
        Me.rdMyFile.Name = "rdMyFile"
        Me.rdMyFile.Size = New System.Drawing.Size(114, 18)
        Me.rdMyFile.TabIndex = 3
        Me.rdMyFile.TabStop = True
        Me.rdMyFile.Text = "My Application"
        Me.rdMyFile.UseVisualStyleBackColor = True
        '
        'rdPending
        '
        Me.rdPending.AutoSize = True
        Me.rdPending.Location = New System.Drawing.Point(108, 12)
        Me.rdPending.Name = "rdPending"
        Me.rdPending.Size = New System.Drawing.Size(76, 18)
        Me.rdPending.TabIndex = 2
        Me.rdPending.TabStop = True
        Me.rdPending.Text = "Pending"
        Me.rdPending.UseVisualStyleBackColor = True
        '
        'rdApproved
        '
        Me.rdApproved.AutoSize = True
        Me.rdApproved.Location = New System.Drawing.Point(10, 12)
        Me.rdApproved.Name = "rdApproved"
        Me.rdApproved.Size = New System.Drawing.Size(85, 18)
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
        Me.btnRefresh.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.btnRefresh.Hint = ""
        Me.btnRefresh.Location = New System.Drawing.Point(329, 561)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Scheme = PinkieControls.ButtonXP.Schemes.Blue
        Me.btnRefresh.Size = New System.Drawing.Size(120, 32)
        Me.btnRefresh.TabIndex = 160
        Me.btnRefresh.Text = "Refresh"
        '
        'btnDisapprove
        '
        Me.btnDisapprove.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDisapprove.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnDisapprove.DefaultScheme = False
        Me.btnDisapprove.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnDisapprove.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.btnDisapprove.Hint = ""
        Me.btnDisapprove.Location = New System.Drawing.Point(575, 561)
        Me.btnDisapprove.Name = "btnDisapprove"
        Me.btnDisapprove.Scheme = PinkieControls.ButtonXP.Schemes.Blue
        Me.btnDisapprove.Size = New System.Drawing.Size(120, 32)
        Me.btnDisapprove.TabIndex = 162
        Me.btnDisapprove.Text = "Disapprove"
        '
        'btnApprove
        '
        Me.btnApprove.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnApprove.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnApprove.DefaultScheme = False
        Me.btnApprove.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnApprove.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.btnApprove.Hint = ""
        Me.btnApprove.Location = New System.Drawing.Point(452, 561)
        Me.btnApprove.Name = "btnApprove"
        Me.btnApprove.Scheme = PinkieControls.ButtonXP.Schemes.Blue
        Me.btnApprove.Size = New System.Drawing.Size(120, 32)
        Me.btnApprove.TabIndex = 161
        Me.btnApprove.Text = "Approve"
        '
        'ColLeaveFilingId
        '
        Me.ColLeaveFilingId.DataPropertyName = "LeaveFilingId"
        Me.ColLeaveFilingId.HeaderText = "LeaveFilingId"
        Me.ColLeaveFilingId.Name = "ColLeaveFilingId"
        Me.ColLeaveFilingId.ReadOnly = True
        Me.ColLeaveFilingId.Visible = False
        '
        'ColDateFiled
        '
        Me.ColDateFiled.DataPropertyName = "DateCreated"
        Me.ColDateFiled.HeaderText = "Date Created"
        Me.ColDateFiled.Name = "ColDateFiled"
        Me.ColDateFiled.ReadOnly = True
        Me.ColDateFiled.Width = 120
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
        Me.ColLeaveTypeName.Width = 125
        '
        'ColName
        '
        Me.ColName.DataPropertyName = "Name"
        Me.ColName.HeaderText = "Name"
        Me.ColName.Name = "ColName"
        Me.ColName.ReadOnly = True
        Me.ColName.Width = 200
        '
        'ColStartDate
        '
        Me.ColStartDate.DataPropertyName = "StartDate"
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.ColStartDate.DefaultCellStyle = DataGridViewCellStyle12
        Me.ColStartDate.HeaderText = "Start Date"
        Me.ColStartDate.Name = "ColStartDate"
        Me.ColStartDate.ReadOnly = True
        Me.ColStartDate.Width = 105
        '
        'ColEndDate
        '
        Me.ColEndDate.DataPropertyName = "EndDate"
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.ColEndDate.DefaultCellStyle = DataGridViewCellStyle13
        Me.ColEndDate.HeaderText = "End Date"
        Me.ColEndDate.Name = "ColEndDate"
        Me.ColEndDate.ReadOnly = True
        Me.ColEndDate.Width = 105
        '
        'ColQuantity
        '
        Me.ColQuantity.DataPropertyName = "Quantity"
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.ColQuantity.DefaultCellStyle = DataGridViewCellStyle14
        Me.ColQuantity.HeaderText = "Qty"
        Me.ColQuantity.Name = "ColQuantity"
        Me.ColQuantity.ReadOnly = True
        Me.ColQuantity.Width = 60
        '
        'ColReason
        '
        Me.ColReason.DataPropertyName = "Reason"
        Me.ColReason.HeaderText = "Reason"
        Me.ColReason.Name = "ColReason"
        Me.ColReason.ReadOnly = True
        Me.ColReason.Width = 265
        '
        'ColClinicIsApproved
        '
        Me.ColClinicIsApproved.DataPropertyName = "ClinicIsApproved"
        Me.ColClinicIsApproved.HeaderText = "Clinic Is Approved"
        Me.ColClinicIsApproved.Name = "ColClinicIsApproved"
        Me.ColClinicIsApproved.ReadOnly = True
        Me.ColClinicIsApproved.Visible = False
        '
        'ColClinicClearance
        '
        DataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.ColClinicClearance.DefaultCellStyle = DataGridViewCellStyle15
        Me.ColClinicClearance.HeaderText = "Clinic"
        Me.ColClinicClearance.Name = "ColClinicClearance"
        Me.ColClinicClearance.ReadOnly = True
        Me.ColClinicClearance.Width = 80
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
        Me.ColRoutingStatusName.HeaderText = "Status"
        Me.ColRoutingStatusName.Name = "ColRoutingStatusName"
        Me.ColRoutingStatusName.ReadOnly = True
        Me.ColRoutingStatusName.Width = 230
        '
        'frmLeaveList
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(1314, 601)
        Me.Controls.Add(Me.btnDisapprove)
        Me.Controls.Add(Me.btnApprove)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.grpStatus)
        Me.Controls.Add(Me.dgvList)
        Me.Controls.Add(Me.grpCriteria)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnView)
        Me.Controls.Add(Me.btnFileLeave)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Verdana", 8.5!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLeaveList"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "List of Leave"
        Me.grpCriteria.ResumeLayout(False)
        Me.grpCriteria.PerformLayout()
        CType(Me.bindingNavigator, System.ComponentModel.ISupportInitialize).EndInit()
        Me.bindingNavigator.ResumeLayout(False)
        Me.bindingNavigator.PerformLayout()
        Me.pnlDateSearch.ResumeLayout(False)
        Me.pnlDateSearch.PerformLayout()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpStatus.ResumeLayout(False)
        Me.grpStatus.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnClose As PinkieControls.ButtonXP
    Friend WithEvents btnView As PinkieControls.ButtonXP
    Friend WithEvents btnFileLeave As PinkieControls.ButtonXP
    Friend WithEvents grpCriteria As System.Windows.Forms.GroupBox
    Friend WithEvents pnlDateSearch As System.Windows.Forms.Panel
    Friend WithEvents lblStartFrom As System.Windows.Forms.Label
    Friend WithEvents btnResetDate As System.Windows.Forms.Button
    Friend WithEvents btnSearchDate As System.Windows.Forms.Button
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblStartTo As System.Windows.Forms.Label
    Friend WithEvents lblCriteria As System.Windows.Forms.Label
    Friend WithEvents cmbSearchCriteria As System.Windows.Forms.ComboBox
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
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnGo As System.Windows.Forms.ToolStripButton
    Friend WithEvents ColLeaveFilingId As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColDateFiled As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColLeaveTypeId As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColLeaveTypeName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColStartDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColEndDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColQuantity As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColReason As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColClinicIsApproved As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColClinicClearance As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RoutingStatusId As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColRoutingStatusName As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
