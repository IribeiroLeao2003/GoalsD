   M S B A M L   `   `   `  ����H  DDailyGoalsapp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:DailyGoalsappDailyGoalsapp  5   	       DailyGoalsapp.MainPage   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:DailyGoalsapp     5   	     � Title$  MainPage��5   	   $	��450��6   $	��800��6'   .��5	      � 
�������$��Center=�6%   $	��800��6B   .+�5   
   �� -   5         ��Name$ AllTheGoals��5      $��Center=�6'   $��0,70,0,0q�6D   $	��Top=�6V   $	��247��6n   $	��516��6{   
���������5      �� 5      .R�5      �  � ColumnDefinitions 5      �� 5      $��* �6/   �� 5      $
��Auto �6/   .+�5      �� $��0��6$   $��Center=�6N   ��64   �� *
GoalText+6N   �� 5      $��1��6#   $��Center=�6S    d� 	IsChecked 63   �� *	IsChecked+6S   5      5      5      �� 5   
   -   5         ��Name$ GoalsNow��5      $��Center=�6&   $��0,35,0,0q�6C   $
��Wrap=�6U   $	��Top=�6i   �� 6�    � Language$ en-ca�6�   $�Current Goals��6�   6�   �� 5   
   -   5        $ MessageNoGoal��5      $��Center=�6+   $��	0,112,0,0q�6H   $
��Wrap=�6[   $	��Top=�6o   $��83��6�   $	��394��6�   $��20(�6�   �� 6�   $ en-ca�6�   $V�P               You have no goals now :( 
                     Lets change that !��6�   6#  �� 5   
   -   5         ��Name$ AddAGoal��5      $��Add Goal��6#   
��� � �$��Center=�6I   $��	0,217,0,0q�6f   $	��Top=�6y   6�   �� 5   
   -   5        $ 
AddnewGoal��5      $��Add New Goal��6%   
��� � �$
��Left=�6P   $��
68,373,0,0q�6k   $	��Top=�6   6�   �� 5   
   -   5        $ 
DeleteGoal��5      $��Delete Goal��6%   
���  ��$
��Left=�6L   $��
68,393,0,0q�6g   $	��Top=�6{   $��81��6�   6�   5      5      