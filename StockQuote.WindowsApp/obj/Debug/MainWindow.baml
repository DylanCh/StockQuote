   M S B A M L   `   `   `  ����P  LStockQuote.WindowsApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:StockQuote.WindowsAppStockQuote.WindowsApp  5   	   &     StockQuote.WindowsApp.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:StockQuote.WindowsApp     5   	     � Title$  
StockQuote��5   	   $��495.35��6   $	��616��6,   .��5	      � .+�5
   
   �� $
��Left=�6   $	��277��60   $��	24,20,0,0q�6=   $	��Top=�6P   $	��108��6h   .+�5      �� $	��100��6   .+�5      �� -   5         ��Name$ groupBox��5      $��	0,0,-36,0q�6-   $��82��6@   $��Select Your Market��5   '   .��5      	� -   5         	�Name$ Sp500��5   "    	� 	GroupName$ markets��61   $��S&P 500��6E    d� 	IsChecked$
 TrueY�6[   5      5      5      �� 5   
   $
��Left=�6   $	��211��60   $��
216,20,0,0q�6=   $	��Top=�6Q   $	��234��6i   .+�5      �� -   5         ��Name$ label��5      $��Symbol (e.g. MSFT)��6#   �� 5      -   5         ��Name$ textBox��5      $��23��6'    �� TextWrapping$
 Wrap=�63   $�� ��6G   .��6q   ��5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5      �� 6   6sys&clr-namespace:System;assembly=mscorlib     6    �� 
TargetType" ��6O   	 �� 	Resources	 5         CueBannerBrush
 j� 
AlignmentX j� 
AlignmentY j� Stretch !� Visual��%�   &	        !� 5      $

 Left=�6A   $ Center=�6S   $
 None=�6g    5   "   �� 5   &   $��Query (e.g. MSFT)��6,   
�������6H   5!      5"      5#      .��5$   !   5%      �� 5&      -   5        $ label2��5&      .��5'      �� -   5        $ label3��5'      $��From:��6(   5(      (  "System.Windows.Controls.DatePicker  5)      -   5          Name$ fromDate��5)         SelectedDate 6+   �� *	from_date+6N   �� 5*      -   5        $ label5��5*      $	��To:��6$     5+      -	   5        $ toDate��5+       6)   �� *	to_date+6J   �� 5,      -
   5         ��Name$ isWeekly��5,      $��Weekly Data��6)   �� 5-      -   5         ��Name$ button��5-      .��5.      �� -   5        $ label1��5.      $��Search��6(   5/      50      �� 51   
   $
��Left=�6   $	��213��60   $��
44,231,0,0q�6=   $	��Top=�6Q   $	��538��6i   .+�52      �� -   5        $ label4��52      .��53      �� -   5        $ label6��53      $�� ��6(   $y�Hidden=�63   54      �� 55      -   5         ��Name$ listView��55      $	��100��6)   $y�	Collapsed=�66   ��6M   �� *DATA_SERVICE+.@�5B      &   System.Windows.Controls.DataGrid  -   5          Name$ dataGrid��5B      "   VerticalScrollBarVisibility$
 Auto=�6-      AutoGenerateColumns .  ��6l   �� *DATA_SERVICE+   Columns 5C      0  *System.Windows.Controls.DataGridTextColumn  5D      ,  &System.Windows.Controls.DataGridColumn   Header$ Date ��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$
 auto 6<   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6I   ��  �� Path$
 Date�  5E      $ Open ��6-   $
 auto 6<    6I   �� $
 Open�  5F      $ High ��6-   $
 auto 6<    6I   �� $
 High�  5G      $
 Low ��6-   $
 auto 6;    6H   �� $	 Low�  5H      $ Close ��6-   $
 auto 6=    6J   �� $ Close�  5I      $ Volumn ��6-   $
 auto 6>    6K   �� $ Volume�  5J      $ Adjusted Closing��6-   $
 auto 6G    6T   �� $ 	Adj_Close�5L      5M      �� 5N      -   5        $ button1��5N      $��
View Chart��6&   $y�	Collapsed=�6;   6R   5O      5R      5S      