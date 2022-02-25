# -*- coding: utf-8 -*-

from __future__ import print_function
import time
import sys
import pygame
import math
import threading
import signal
from pygame.locals import *
from WHILL_Command import WHILL

class JoystickPS4:
    # buttons
    UP = 1
    RIGHT = -1
    DOWN = -1
    LEFT = -1
    L2 = 6
    R2 = 7
    L1 = 4
    R1 = 5
    TRIANGLE = 2
    CIRCLE = 1
    CROSS = 0
    SQUARE = 3
    # axis
    LEFT_X = 0
    LEFT_Y = 1
    RIGHT_X = 3
    RIGHT_Y = 4
    LEFT_X_REVERSE = 1.0
    LEFT_Y_REVERSE = -1.0
    RIGHT_X_REVERSE = 1.0
    RIGHT_Y_REVERSE = -1.0
    BACKSLASH = 0.08

stop_flg = 0
MAX_SPEED = 120
Fw = 0.0

def name():
    pygame.init()
    lightState = False
    screen = pygame.display.set_mode((480, 360))
    name = ""
    font = pygame.font.Font(None, 50)
    whill = WHILL('/dev/ttyUSB0')
    buttons = None
    # Fw = 0.0
    LR = 0.0
    # MAX_SPEED = 60
    SLM = False
    WDT = 10
    interval = 0.3

    global event
    event = threading.Event()
    signal.signal(signal.SIGALRM, int_timer)
    signal.setitimer(signal.ITIMER_REAL, interval, interval)

    stop_time = 10
    thread = threading.Thread(target=stop_motion)
    thread_accel = threading.Thread(target=acceleration, args=(event,))
    thread_decel = threading.Thread(target=deceleration, args=(event,))
    setting_flg = 1
    acc_flg = 0
    acc_cnt = 0
    a = 0.0
    global MAX_SPEED
    global Fw

    while buttons is None:
        pygame.joystick.init()
        try:
            js = pygame.joystick.Joystick(0)
            js.init()
            js_name = js.get_name()

            if js_name in 'Wireless Controller':
                buttons = JoystickPS4
            print('Joystick name: ' + js_name)
            print("numbuttons: %d" % js.get_numbuttons())
            print("numaxes:%d" % js.get_numaxes())
            print("numballs:%d" % js.get_numballs())
            print("numhats:%d" % js.get_numhats())
        except pygame.error:
            print('No controller')
            time.sleep(5)
            pass

    if buttons is None:
        print('no supported joystick found')
        return

    _quit = False
    whill.SetMaxSpeed()

    while not _quit:
        time.sleep(0.01)
        try:
            js = pygame.joystick.Joystick(0)
            js.init()
            js.get_name()
        except:
            print('No controller')
            sys.exit()

        for evt in pygame.event.get():
            if evt.type == 9:  # (KEYDOWN = 2)
                if evt.key == K_UP:
                    name = 'Foward'
                    whill.SetJoystick(0, 100, 0)
                elif evt.key == K_DOWN:
                    name = 'Backward'
                    whill.SetJoystick(0, -120, 0)
                elif evt.key == K_LEFT:
                    name = 'Left'
                    whill.SetJoystick(0, 0, -100)
                elif evt.key == K_RIGHT:
                    name = 'Right'
                    whill.SetJoystick(0, 0, 100)
                elif evt.key == K_ESCAPE:
                    _quit = True
                    pygame.quit()
                    return
            elif evt.type == pygame.locals.JOYBUTTONDOWN:  # ( = 10)
                if evt.button == buttons.L1:
                    MAX_SPEED -= 10;
                    if MAX_SPEED < 0:
                        MAX_SPEED = 0

                # if evt.button == buttons.L2:
                #	thread_decel.start()

                if evt.button == buttons.R1:
                    MAX_SPEED += 10;
                    if MAX_SPEED > 120:
                        MAX_SPEED = 120
                # if evt.button == buttons.R2:
                #	if acc_flg == 0:
                #		acc_flg = 1
                #	else:
                #		acc_flg = 0

                if evt.button == buttons.TRIANGLE:
                # Fw = 62 #for constant speed
                Fw = 47  # for acceleration
                # Fw = 77 #for deceleration

                if evt.button == buttons.SQUARE:
                    Fw = 0

                if evt.button == buttons.CIRCLE:
                    thread_accel.start()
                    # thread_decel.start()

                if evt.button == buttons.CROSS:
                    global stop_flg
                    stop_flg = 1
                    Fw = 0

            elif evt.type == pygame.JOYAXISMOTION:
                if SLM == False:
                    if evt.axis == buttons.RIGHT_X:
                        if (setting_flg):
                            if (evt.value > 0.1 or evt.value < -0.1):
                                LR = evt.value * MAX_SPEED
                            else:
                                LR = 0.0
                    if evt.axis == buttons.LEFT_Y:
                        if (evt.value > 0.1 or evt.value < -0.1):
                            Fw = evt.value * -MAX_SPEED

                        # else:
                        #	Fw = 0.0
                name = 'JOYSTICK'

            elif evt.type == QUIT:  # QUIT = 12
                whill.SetPower(0)
                pygame.quit()
                return


        global Fw
        print(evt.type)
        print('\r MOVE: %f %f MAX:%d acc:%d     ' % (Fw, a, MAX_SPEED, acc_flg), end='')
        global stop_flg
        whill.SetJoystick(0, int(Fw), int(LR))

        screen.fill((0, 0, 0))
        block = font.render(name, True, (255, 255, 255))
        rect = block.get_rect()
        rect.center = screen.get_rect().center
        screen.blit(block, rect)
        pygame.display.flip()


def stop_motion():
    time.sleep(10)
    global stop_flg
    stop_flg = 1


def int_timer(signum, stack):
    event.set()


def acceleration(event):
    for i in range(100):
        event.wait()
        event.clear()
        global Fw
        Fw += 1
        global stop_flg
        if stop_flg == 1:
            break


def deceleration(event):
    for i in range(100):
        event.wait()
        event.clear()
        global Fw
        Fw -= 1
        global stop_flg
        if stop_flg == 1:
            break


if __name__ == "__main__":
    name()