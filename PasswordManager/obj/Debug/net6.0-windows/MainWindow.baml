   M S B A M L   `   `   `  ����J  FPasswordManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:MyApp.ToolsMyApp.Tools  5	   	        PasswordManager.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +toolsclr-namespace:MyApp.Tools     5	   	   -   5        $
��home��5   	     � Title$  
MainWindow��5   	    � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$
 None=�5   	   
������  � AllowsTransparency$
 True��5   	    � 
ResizeMode$ NoResize=�5   	    � WindowState$ Normal=�5   	    � Icon$ \Assets\Icons\logo.ico��5   	   ��5
   	   ��  �� Source# Z�� �� 	Converter     MyApp.Tools.RatioConverter  	 �� ConverterParameter$		 0.9����5   	   �� # Z��   $		 0.9��.��5      �� 
 �� CornerRadius$
 8��6      PrimaryBackgroundColor#���   6   ._�5   
   �  � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      .+�5      �� $
 8 0 0 8��6     SecundaryBackgroundColor#���  6,   ._�5      � -   5         �Name$ gridMenu��5      $��60��6)    � RowDefinitions 5      �� 5      $��60 �6(   �� 5      $��80 �6(   �� 5      $
��Auto �6(   �� 5       .+�5$      �� $��1��6!    �� Orientation$ 
Horizontal=�6.   $	��Top=�6G   .+�5%      F�   logo#���  6   $��16 0 0 0q�6=    �� Stretch$
 Fill=�6O     PrimaryBlueColor#���  6^   $��25��6�   $��25��6�   $
��Left=�6�   �� 5&      $��ITAPass��6    #���  62   $
��Bold"�6b   $��20(�6t   $��Center=�6�   $
��Left=�6�   $
��Left=�6�   $��14 0 0 0q�6�   5'      d� 5*      -   5         d�Name$ btnMenu��5*   #   $��Right=�65   $��0q�6Q   
������ $��Center=�6~     MenuIconStyle#��[ 6�   $��0 0 15 0q�6�    � Triggers 5+      :� 5,       :� RoutedEvent$ ToggleButton.Unchecked��6+   .[�5-   "   �� .��5.   &   �� -   5         ��Name$ HideStackPanel��5.   1   .��5/   *   W�  �� 
TargetName$ gridMenu��6:    �� TargetProperty$ Width�6[    g� 	BeginTime	 0:0:06}    W� From 2006�   	 W� To 606�    g� Duration$ 0:0:0.3D�6�   50   +   51   '   52   #   53      :� 54      $ ToggleButton.Checked��6+   .[�55   "   �� .��56   &   �� -   5        $ ShowStackPanel��56   1   .��57   *   W� $ gridMenu��6:   $ Width�6[   	 0:0:06}    606�    2006�   $ 0:0:0.3D�6�   58   +   59   '   5:   #   5;      5=      �� 5?      $��3��6!   .+�5@      	� -   5         	�Name$ rdHome��5@   &    d� 	IsChecked$
 TrueY�66   $
��Home��6G     MenuRadioButtonStyle#���  6V     home
 � Tag# �  6�   6�   	� 5A      -   5        $ rdPasswords��5A   &   $��	Passwords��6;   #���  6O     	passwords# �  6~   $��0 5 0 5q�6�    � Cursor$
 Hand��6�   5B      5C      5D      � 5H      $��1��6    5I      �� 5J      $��60 �6$   �� 5K      .+�5O      �� $��0��6   $��4��6*   $��Right=�6:   $	��Top=�6V   $��0,5,10,0q�6n   $��30��6�   $ 
Horizontal=�6�   $��RightToLeft=�6�   .+�5P      �� -   5         ��Name$ btnClose��5P        IconButtonsStyle#��[ 6/    		 close#��� 	 6Y   $ IsCloseButton��6�   $
 Hand��6�   �� 5Q      -	   5        $ btnMinimize��5Q      #���  62    
 minimize#��� 
 6]   6�   5R      �� 5U      $��1��6   $��10q�6&   ._�5V       � -
   5          �Name$ PagesNavigation��5V         � NavigationUIVisibility$  Hidden=�65   5X      5Y      5Z      5\      5]      5^      