   M S B A M L   `   `   `  ����  PasswordManager)    #PasswordManager.Pages.PasswordsPage   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      b ^MaterialDesignThemes.Wpf, Version=4.1.0.2556, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5        N� Title$  
SoundsPage��5	      .��5
      �  � ColumnDefinitions 5   
   �� 5      $	��87* �6   �� 5      $	��13* �6   .+�5   
   ��  �� CornerRadius$ 5��6      SecundaryBackgroundColor#���   6"   $��2��6Z   �� 5   
   $��2��6   .��5      � .+�5      �� $��32,10,351,405q�6   $	��400��5      $��25(�5      $��Your Passwords��5       �� TextAlignment$ Center=�5      $
��Bold"�5      �� 5      -   5         ��Name$ ListViewPasswords��5      $��32,45,391,10q�5      $��15(�5       �� View 5      �� 5      .P�5      �� $	��Use��6-    �� DisplayMemberBinding 6:   �� *Use+�� 5      $��Password��6-    6?   �� *
Password+�� 5       $	��URL��6-    6:   �� *URL+5!      5#      5$      5%      �� 5&   
   -   5         ��Name$ btn_add_password��5'      $��658,29,-158,368q�5&      $��53��5(      $	��300��5)        MaterialDesignFlatButton#��[ 5*      +  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth0=�5+      +  %MaterialDesignThemes.Wpf.ButtonAssist	   CornerRadius$	 10��5,      $��18(�5-      $��Add Password��5.      
 � Cursor$

 Hand��6(   $��2��6R   �� 5/   
   -   5        $ btn_remove_all_passwords��50      $��658,87,-158,310q�5/      $��53��51      $	��300��52      #��[ 53      $ Depth0=�54      $	 10��55      $��18(�56      $��Remove All Passwords��57      $

 Hand��60   $��2��6e   �� 58   
   -   5        $ 
btn_update��59      $��658,359,-158,38q�58      $��53��5:      $	��300��5;      #��[ 5<      $ Depth0=�5=      $	 10��5>      $��18(�5?      $��Update List��5@      $

 Hand��6'   $��2��6M   �� 5A   
   -   5        $ btn_generate_password��5D      $��658,223,-158,174q�5A      $��53��5B      $	��300��5C      #��[ 5E      $ Depth0=�5F      $	 10��5G      $��18(�5H      $��Generate Password��5I      $

 Hand��6-   $��2��6Y   �� 5J   
   -   5        $ btn_copy_password��5K      $��658,281,-158,116q�5J      $��53��5L      $	��300��5M      #��[ 5N      $ Depth0=�5O      $	 10��5P      $��18(�5Q      $��Copy Passwords��5R      $

 Hand��6*   $��2��6R   �� 5S   
   -   5        $# btn_remove_all_passwords_Copy��5T      $��658,145,-158,252q�5S      $��53��5U      $	��300��5V      #��[ 5W      $ Depth0=�5X      $	 10��5Y      $��18(�5Z      $��Remove Password��5[      $

 Hand��6+   $��2��6U   5\      5]      