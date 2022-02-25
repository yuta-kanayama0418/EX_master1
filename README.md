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
  
