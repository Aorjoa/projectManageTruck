   M S B A M L   `   `   `  ����J  FWpfApplication2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WpfApplication2WpfApplication2  5   	   %    WpfApplication2.PaidTransaction   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:WpfApplication2     5   	     � Title$  PaidTransaction��5   	   $��332.667��6!   $��465.475��62    � Icon$ trucking.ico��6B   .��5	      � $
��Left=�6   $	��301��6&   $	��Top=�63   $	��466��6K   $��	0,0,-58,0q�6W   .+�5
   
   �� -   5         ��Name$ label��5
      $,��&เลขที่รายการ :��6   $
��Left=�68   $��	37,10,0,0q�6S   $	��Top=�6f   �� 5   
   -   5         ��Name$ inputRecordId��5      $
��Left=�6)   $��23��6D   $��
116,13,0,0q�6P    �� TextWrapping$
 Wrap=�6d   $	��Top=�6x   $	��234��6�   �� 5   
   -   5         ��Name$ searchRecord��5      $��ค้นหา��6'   $
��Left=�67   $��
355,13,0,0q�6R   $	��Top=�6f   $��46��6~   $��23��6�   6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dataGrid��5      $
��Left=�6%   $��	10,50,0,0q�6@   $	��Top=�6S   $��97��6k   $	��421��6w   �� 5   
   -   5         ��Name$ groupBox��5      $*��$แจ้งชำระเงิน��6%   $
��Left=�6;   $��
10,157,0,0q�6V   $	��Top=�6j   $	��124��6�   $	��421��6�   ���	#FF298787.��5      � $
��Left=�6   $	��105��6.   $��
0,0,-45,-3q�6;   $	��Top=�6O   $	��411��6g   .+�5      �� -   5        $ lblRecoardId��5      $&�� <กรุณาค้นหา>��6.   $
��Left=�6K   $��89,2,0,0q�6f   $	��Top=�6x   $	��111��6�   �� 5      -   5        $ 
label_Copy��5      $,��&เลขที่รายการ :��6,   $
��Left=�6E   $��14,2,0,0q�6`   $	��Top=�6r   �� 5      -   5        $ label_Copy1��5      $ ��ค้างชำระ :��6-   $
��Left=�6B   $��	33,23,0,0q�6]   $	��Top=�6p   �� 5      -	   5        $ lblRemainCost��5      $&�� <กรุณาค้นหา>��6/   $
��Left=�6L   $��	88,23,0,0q�6g   $	��Top=�6z   $	��111��6�   �� 5      -
   5        $ label_Copy2��5      $;��5ชำระเงินเพิ่มเติม :��6-   $
��Left=�6K   $��0,45,0,0q�6f   $	��Top=�6x   �� 5      -   5        $ inputPaidAmont��5      $
��Left=�62   $��23��6M   $��	89,48,0,0q�6Y   $
 Wrap=�6l   $	��Top=�6�   $	��120��6�   �� 5      -   5        $ payMore��5      $"��$ ชำระเงิน $��6*   $
��Left=�6A   $��
316,29,0,0q�6\   $	��Top=�6p   $��75��6�   $��42��6�   6�   �� 5      -   5        $ label_Copy3��5      $2��,วันที่ชำระเงิน :��6-   $
��Left=�6H   $��	15,69,0,0q�6c   $	��Top=�6v   (  "System.Windows.Controls.DatePicker  5      -   5          Name$ paidDate��5      $
��Left=�6/   $��	89,76,0,0q�6J   $	��Top=�6]   $	��120��6u   5      5      5      5      