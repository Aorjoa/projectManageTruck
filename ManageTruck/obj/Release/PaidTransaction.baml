   M S B A M L   `   `   `  ���� J  FWpfApplication2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WpfApplication2WpfApplication2  %    WpfApplication2.PaidTransaction   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     /localclr-namespace:WpfApplication2       � Title$  PaidTransaction��$��332.667��$��465.475�� � Icon$ trucking.ico��.��� $
��Left=�$	��301��$	��Top=�$	��466��$��	0,0,-58,0q�.+��� -    ��Name$ label��$,��&เลขที่รายการ :��$
��Left=�$��	37,10,0,0q�$	��Top=��� -    ��Name$ inputRecordId��$
��Left=�$��23��$��
116,13,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��234���� -    ��Name$ searchRecord��$��ค้นหา��$
��Left=�$��
355,13,0,0q�$	��Top=�$��46��$��23��&   System.Windows.Controls.DataGrid  -     Name$ dataGrid��$
��Left=�$��	10,50,0,0q�$	��Top=�$��97��$	��421���� -    ��Name$ groupBox��$*��$แจ้งชำระเงิน��$
��Left=�$��
10,157,0,0q�$	��Top=�$	��124��$	��421�����	#FF298787.��� $
��Left=�$	��105��$��
0,0,-45,-3q�$	��Top=�$	��411��.+��� -   $ lblRecoardId��$&�� <กรุณาค้นหา>��$
��Left=�$��89,2,0,0q�$	��Top=�$	��111���� -   $ 
label_Copy��$,��&เลขที่รายการ :��$
��Left=�$��14,2,0,0q�$	��Top=��� -   $ label_Copy1��$ ��ค้างชำระ :��$
��Left=�$��	33,23,0,0q�$	��Top=��� -	   $ lblRemainCost��$&�� <กรุณาค้นหา>��$
��Left=�$��	88,23,0,0q�$	��Top=�$	��111���� -
   $ label_Copy2��$;��5ชำระเงินเพิ่มเติม :��$
��Left=�$��0,45,0,0q�$	��Top=��� -   $ inputPaidAmont��$
��Left=�$��23��$��	89,48,0,0q�$
 Wrap=�$	��Top=�$	��120���� -   $ payMore��$"��$ ชำระเงิน $��$
��Left=�$��
316,29,0,0q�$	��Top=�$��75��$��42���� -   $ label_Copy3��$2��,วันที่ชำระเงิน :��$
��Left=�$��	15,69,0,0q�$	��Top=�(  "System.Windows.Controls.DatePicker  -     Name$ paidDate��$
��Left=�$��	89,76,0,0q�$	��Top=�$	��120��