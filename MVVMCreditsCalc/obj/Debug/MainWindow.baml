   M S B A M L   `   `   `  ����  CreditCalc v1.1     MVVMCreditsCalc.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0ignorehttp://www.galasoft.ch/ignore     5   	   $	��440��5   	   $	��840��5	   	     � Title$  CreditCalc v1.1��5
   	    � DataContext 5   	   �� *Main+   Locator �� Source# [   � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $ Skins/MainSkin.xaml?�6%   %    5      .��5      � .+�5   
   � $
��Left=�6   $	��374��6+   $��	10,25,0,0q�68   $	��Top=�6K   $	��248��5      .+�5      �� -   5         ��Name$ labelSumCred��5      $ ��Сумма кредита:��6*   $
��Left=�6C   $��	10,10,0,0q�6^   $	��Top=�6q   �� 5      -   5         ��Name$ 
tbxSumCred��5      $
��Left=�6*   $��23��6E   $��
118,13,0,0q�6Q   $	��Top=�5      $	��120��6/   ��5      �� *
Calc.Sum+ �� Mode$ TwoWay=�	 �� UpdateSourceTrigger$	 PropertyChanged=�
 �� ValidatesOnDataErrors
 . �� 5      -   5        $ labelDataZaim��5      $'��!Дата начала займа:��6+   $
��Left=�6H   $��	10,41,0,0q�6c   $	��Top=�6v   (  "System.Windows.Controls.DatePicker  5      -   5          Name$ dtPicker��5      $
��Left=�6+   $��
131,43,0,0q�6F   $	��Top=�5      $	��107��61      IsTodayHighlighted .    SelectedDate 5      �� *Calc.DataZaima+$ TwoWay=�$	 PropertyChanged=��� 5       -   5        $ labelSrokPogash��5       $"��Срок погашения:��6-   $
��Left=�6G   $��	10,72,0,0q�6b   $	��Top=�5!      �� 5"      -   5         ��Name$ comboBox��5"      $
��Left=�6)   $��
119,76,0,0q�6D   $	��120��5%      $��22��6#   $	��Top=�6/   $ �� IsSynchronizedWithCurrentItem$
 TrueY�6G   ��5#      �� *Calc.Sroks.Keys+$ OneWay=�$	 PropertyChanged=� �� SelectedValue 5$      �� *Calc.CbSrokTmp+$ TwoWay=�$	 PropertyChanged=�5%   G   �� 5&      -   5        $ labelTypeValuteCred��5&      $)��#Тип валюты кредита:��61   $
��Left=�6O   $��
10,169,0,0q�6j   $	��Top=�6~   	� 5'      -   5         	�Name$ rbRub��5'      $	��RUB��6)   $
��Left=�67   $��
10,200,0,0q�5(      $	��Top=�6.    d� 	IsChecked 5)      ��  �� Path$ Calc.Prc�$ TwoWay=� �� ConverterParameter$ 16��  ConvertBoolToCurrncy �� 	Converter# [ $	 PropertyChanged=�5-      	� 5.      -	   5        $ rbUsd��5.      $	��USD��6)   $
��Left=�67   $��
75,200,0,0q�5/      $	��Top=�6.    50      �� $ Calc.Prc�$ TwoWay=�$ 9��# [ $	 PropertyChanged=�	� 54      -
   5        $ rbEur��54      $	��EUR��6)   $
��Left=�67   $��142,200,0,0q�55      $	��Top=�6/    56      �� $ Calc.Prc�$ TwoWay=�$ 6��# [ $	 PropertyChanged=��� 59      -   5        $ labelProcent��59      $(��"Процентная ставка:��6*   $
��Left=�6G   $��
13,220,0,0q�6b   $	��Top=�6v   �� 5:      -   5        $ tbPrc��5:      $��23��6%   $��136,223,85,0q�61   $	��Top=�5;       �� TextAlignment$ Center=�5=      ��5<      �� *
Calc.Prc+$ TwoWay=�$	 PropertyChanged=�5=      �� 5>      -   5        $ label��5>      $��%��6#   $
��Left=�6/   $��168,220,0,0q�6J   $	��Top=�6_   �� 5?      -   5         ��Name$ btnCalculation��5?      $��Рассчитать��6-   $��10,251,10,0q�6B   $	��Top=�6W   $��59��5@      ��6!   �� *CalculateCrediit+�� 5A      -   5        $ btnSave��5A      $��Сохранить��6&   $
��Left=�6:   $��
10,315,0,0q�6U   $	��Top=�5B      $	��107��6-   $��49��69   ��5C      �� *CalculateSave+�� 5D      -   5        $ btnClear��5D      $��Очистить��6'   $
��Left=�6:   $��131,315,0,0q�5E   6   $	��Top=�6K   $	��107��6c   $��49��6o   ��6   �� *CalculateDel+6o   	� 5F      -   5        $ rbAnnuit��5F       	� 	GroupName$
 Type��6,   $��ануитентный��6=   $��103,124,55,0q�5G      $	��Top=�60    5H      �� *Calc.TrueFalseAnnuit+$ TwoWay=�$	 PropertyChanged=�	� 5I      -   5        $ rbDiffer��5I      $
 Type��6,   $&�� дифференцируемый��6=   $��103,144,7,0q�5J      $	��Top=�6/    5K      �� *Calc.TrueFalseDiff+$ TwoWay=�$	 PropertyChanged=��� 5L      -   5        $ labelTypePay��5L      $��Тип выплат:��6*   $
��Left=�6@   $��
13,118,0,0q�6[   $	��Top=�6o   5M      � 5N   
   $��263,25,10,10q�6    � RowDefinitions 5P      �� 5Q      $
��321* �6    �� 5R      $	��53* �6    .+�5T      &   System.Windows.Controls.DataGrid  -   5          Name$ dgTable��5T      $��
10,10,10,1q�6(      CanUserAddRows .     AutoGenerateColumns .     SelectionMode$ Single=�5V   3   $��Stretch=�6J   4  .System.Windows.Automation.AutomationProperties   IsRowHeader .    EnableRowVirtualization .  ��5U      �� *
Calc.Graph+$	 PropertyChanged=�    Columns  5W      0  *System.Windows.Controls.DataGridTextColumn  5X      ,  &System.Windows.Controls.DataGridColumn!   Header$	! №��6)   1  +System.Windows.Controls.DataGridBoundColumn"   Binding" 64   ��   NumerRow# [ $ OneWay=�# �� RelativeSource# �� *FindAncestor+$ �� AncestorType)  #System.Windows.Controls.DataGridRow"$  %   ClipboardContentBinding% 5Z   )   Z�   5[      $! Дата займа��6)   " 6=   �� *Dz+  5\      $! Основной долг��6)   " 6@   �� *	OsnDolg+& �� StringFormat$& C��  5]      $! Задолженость��6)   " 6?   �� *	Zadolzh+$& C��  5^      $! Сумма платежа��6)   " 6@   �� *	SumPlat+$& C��  5_      $+! %Начисленный процент��6)   " 6F   �� *
NachislPrc+$& P��5a      �� 5b      -   5        $ labelLastPay��5b      $3��-Дата последнего платежа:��6*   $��10,4,385,23q�6M   $��1��6b   �� 5d      -   5        $ labelDateLastPay��5d      $��179,4,283,23q�5e   7   $��1��6M   $
��Bold"�6Z   ��5d   .   �� *	Calc.Dz+$ OneWay=�$	 PropertyChanged=�5e   Z   5f      o� 5g   
   -   5        ' o�Name$
' menu��5g      $��20��6   $	��Top=�6)   .@�5h      m� $��Файл��6   .@�5i      m� $
��Open��6   $
��Left=�6)   $	��140��6D   ( m� Command( 5j      �� *CalcOpenCsv+m� 5k      $��Print��6   $
��Left=�6*   $	��140��6E   ( 5l      �� *CalculatePrint+m� 5m      -   5        $
��Exit��5m      $
��Left=�6)   $	��140��6D   6P   5o      m� 5p      $��Помощь��6   5q      5s      5t      