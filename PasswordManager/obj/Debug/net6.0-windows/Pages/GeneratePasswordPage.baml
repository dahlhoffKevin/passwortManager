   M S B A M L   `   `   `  ����  PasswordManager0    *PasswordManager.Pages.GeneratePasswordPage   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      b ^MaterialDesignThemes.Wpf, Version=4.1.0.2556, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5        N� Title$  
SoundsPage��5	      .��5
      � .+�5   
   ��  �� CornerRadius$ 5��6      SecundaryBackgroundColor#���   6"   �� 5   
   -   5         ��Name$! checkBox_special_characters��5      $��10,-28,540,428q�5      $	��250��5      $��50��5      $��20(�5      $��Special Characters��5       d� 	IsChecked$
 TrueY�5      5      �� 5   
   -   5        $ checkBox_digits��5      $��10,8,540,392q�5      $	��250��5      $��50��5      $��20(�5      $��Digits��5      $
 TrueY�5      5      �� 5   
   -   5        $  checkBox_normal_characters��5       $��10,45,540,355q�5      $	��250��5      $��50��5      $��20(�5      $��Normal Characters��5      $
 TrueY�5      5       �� 5!   
   $��	0,0,0,570q�6   $��40��5"      $��Center=�5#      $��28(�5$      $
��Bold"�5%      $��Password Generator��5&      5'      �� 5(   
   -   5         ��Name$ txtGeneratedPassword��5)      $��474,21,26,369q�5(      $��60��5*      $	��300��5+      $��18(�5,      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Your Generated Password��5-      $��2q�5.        MaterialDesignDivider#���  5/       ! MaterialDesignOutlinedTextBox#��[ 50       � Cursor$ IBeam��51      .��6%   52      �� 53   
   -   5         ��Name$ btn_add_password��54      $��10,243,490,154q�53       ! MaterialDesignFlatMidBgButton#��[ 55      +  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth0=�56      $��53��57      $	��300��58      +  %MaterialDesignThemes.Wpf.ButtonAssist	   CornerRadius$	 10��59      $��18(�5:      $��Generate Password��5;      $
 Hand��5<      5=      �� 5>   
   -   5        $ btn_copy_clipboard��5?      $��474,101,26,296q�5>      #��[ 5@      $ Depth0=�5A      $��53��5B      $	��300��5C      $	 10��5D      $��18(�5E      $��Copy To Clipboard��5F      $
 Hand��5G      5H      �� 5I   
   -   5        $ txtPasswordLength��5J      $��10,124,490,266q�5I      $��60��5K      $	��300��5L      $��18(�5M      $ Enter Password Length *��5N      $��2q�5O      #���  5P      #��[ 5Q      $ IBeam��5R      �� 5S   
   $��306,198,0,212q�6   $��40��5T      $
��Left=�5U      $��15(�5V      $��* - Must be filled out��5W      �� 5X   
   -   5        $ btn_back��5Y      $��10,301,490,96q�5X        MaterialDesignFlatButton#��[ 5Z      $ Depth0=�5[      $��53��5\      $	��300��5]      $	 10��5^      $��18(�5_      $��Back to Passwords��5`      $
 Hand��5b      5c      5d      