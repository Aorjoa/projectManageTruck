   M S B A M L   `   `   `  ����J  FWpfApplication2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WpfApplication2WpfApplication2  5   	   "    WpfApplication2.SearchWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:WpfApplication2     5   	     � Title$  SearchWindow��5   	    � Icon$ trucking.ico��6   .��5	      � $��0,0,-1,0q�6   .+�5
   
   �� -   5         ��Name$ groupBox��5
      $$��กล่องค้นหา��6%   $��
60,30,57,0q�69   $	��Top=�6M   $��58��6e   .��5      � $��0,0,-2,-3.96q�6   $	��450��6)   $��39��65   .+�5      �� -   5         ��Name$ seachNow��5      $��ค้นหา��6+   $��0,3.001,8.33,5.999q�6;   $��Right=�6n   $��85.53��6�   �� 5      -   5         ��Name$ 
ctIdSearch��5      $��69.903,10,0,6.96q�6.    �� TextWrapping$
 Wrap=�6H    �� TextAlignment$ Center=�6\   $
��Left=�6s   $	��107��6�   �� 5      -   5         ��Name$ 
label_Copy��5      $&�� ชื่อลูกค้า :��6,   $ ��185.736,6.998,208.86,6.999q�6C   �� 5      -   5        $ label��5      $&�� รหัสลูกค้า :��6'   $
��Left=�6>   $��	9.5,7,0,7q�6Y   �� 5      -   5        $ ctNameSearch��5      $��0,7.998,98.86,8.962q�60   $
 Wrap=�6M   $ Center=�6a   $��Right=�6x   $	��107��6�   �� 5      -   5        $ 
showRecord��5      $*��$แสดงใบรายการ��6-   $
��Left=�6D   $��447,3,-87,0q�6_   $	��Top=�6t   $��90��6�   $��30��6�    J� RenderTransformOrigin$ -0.303,0.179&�6�   5      5      &   System.Windows.Controls.DataGrid  5   
   -   5        	  Name$	 dataGrid��5      $��60,108,57,68q�6%   5      5      