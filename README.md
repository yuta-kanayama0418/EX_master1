# Illusion of Speed Perception in Virtual Reality Environment while in Real Motion
電動車椅子で走行中に聴覚刺激と視覚刺激を与えたときの速度感覚の調査実験時に使用

### pygame_whill_JOY_ky_master.py

WHILLに搭載されているUbuntuで起動すると接続したJoystickコントローラーでWHILLを操作できる  
※スティックを戻しても停止しないので□ボタンを押して停止する

△ボタン：一定速度で走行する  
    l.147 等速条件(0.9m/s)  
    l.148 加速条件  
    l.149 減速条件

◯ボタン：加速(減速)を開始する  
    l.155 加速(0.053m/s^2)  
    l.156 減速(-0.053m/^2)  
    2回押すとスレッドの多重起動によってエラーが起こるので一回一回プログラムを再起動しなければならない（もっといい方法があるかも）
  
 □ボタン：停止する（○ボタンによる加速は停止しない）
 
 ×ボタン：停止する（◯ボタンによる加速も停止）  
     一度押すとそれ以降、加速（減速）はできないのでプログラムの再起動が必要
  
  ### Project/ex_master
  
  Unityのプロジェクトフォルダ　event_collisionオブジェクト内のData_utilで各種設定を行う
  
![aaa](https://user-images.githubusercontent.com/63037880/155871882-de79d7fd-c146-492b-adc1-af2b43abf59e.png)

- Subject: この名前のフォルダに計測データがまとめられる
- Test: 基準の走行を行う時にチェックをつける　9m走行時に音が鳴る
- Sound_flg：聴覚刺激のありなし
- Visual_stimuli：視覚刺激のありなし
- Vehicle_movement：実車両の走行タイプ
    - ACC：加速
    - DEC：減速
    - Eq_acc：等速（刺激は体感速度を上げるもの）
    - Eq_dec：等速（刺激は体感速度を下げるもの）
- Fold_path：Subject名のフォルダを格納するパス
