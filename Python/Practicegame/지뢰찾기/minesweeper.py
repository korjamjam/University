""" mine_sweeper.py - Copyright 2016 Kenichiro Tanaka  """
import sys
from math import floor
from random import randint
import pygame
from pygame.locals import QUIT, MOUSEBUTTONDOWN
import time

WIDTH = 10
HEIGHT = 10
SIZE = 50
NUM_OF_BOMBS = 20
EMPTY = 0
BOMB = 1
OPENED = 2
OPEN_COUNT = 0
CHECKED = [[0 for _ in range(WIDTH)] for _ in range(HEIGHT)]
TIMER = 1000

pygame.init()
SURFACE = pygame.display.set_mode([WIDTH*SIZE+300, HEIGHT*SIZE])
FPSCLOCK = pygame.time.Clock()

print(pygame.font.get_fonts())

def num_of_bomb(field, x_pos, y_pos):
    """ 주위에 있는 폭탄 수를 반환한다 """
    count = 0
    for yoffset in range(-1, 2):
        for xoffset in range(-1, 2):
            xpos, ypos = (x_pos + xoffset, y_pos + yoffset)
            if 0 <= xpos < WIDTH and 0 <= ypos < HEIGHT and \
                field[ypos][xpos] == BOMB:
                count += 1
    return count

def open_tile(field, x_pos, y_pos):
    """ 타일을 오픈 """
    global OPEN_COUNT
    if CHECKED[y_pos][x_pos]:  # 이미 확인된 타일
        return

    CHECKED[y_pos][x_pos] = True

    for yoffset in range(-1, 2):
        for xoffset in range(-1, 2):
            xpos, ypos = (x_pos + xoffset, y_pos + yoffset)
            if 0 <= xpos < WIDTH and 0 <= ypos < HEIGHT and \
                field[ypos][xpos] == EMPTY:
                field[ypos][xpos] = OPENED
                OPEN_COUNT += 1
                count = num_of_bomb(field, xpos, ypos)
                if count == 0 and \
                    not (xpos == x_pos and ypos == y_pos):
                    open_tile(field, xpos, ypos)

def main():
    global TIMER
    
    """ 메인 루틴 """
    smallfont = pygame.font.SysFont("새굴림", 36)
    largefont = pygame.font.SysFont("휴먼옛체", 72)
    message_clear = largefont.render("!!CLEARED!!",
                                     True, (0, 255, 225))
    message_over = largefont.render("게임오버!!",
                                    True, (0, 255, 225))
    message_rect = message_clear.get_rect()
    message_rect.center = (WIDTH*SIZE/2, HEIGHT*SIZE/2)
    game_over = False

    field = [[EMPTY for xpos in range(WIDTH)]
             for ypos in range(HEIGHT)]

    # 폭탄을 설치
    count = 0
    while count < NUM_OF_BOMBS:
        xpos, ypos = randint(0, WIDTH-1), randint(0, HEIGHT-1)
        if field[ypos][xpos] == EMPTY:
            field[ypos][xpos] = BOMB
            count += 1

    while True:
        for event in pygame.event.get():
            if event.type == QUIT:
                pygame.quit()
                sys.exit()
            if event.type == MOUSEBUTTONDOWN and \
                event.button == 1:
                xpos, ypos = floor(event.pos[0] / SIZE),\
                             floor(event.pos[1] / SIZE)

                if xpos < WIDTH :
                    if field[ypos][xpos] == BOMB:
                        game_over = True
                    else:
                        open_tile(field, xpos, ypos)

        # 그리기
        SURFACE.fill((0, 0, 0))
        for ypos in range(HEIGHT):
            for xpos in range(WIDTH):
                tile = field[ypos][xpos]
                rect = (xpos*SIZE, ypos*SIZE, SIZE, SIZE)

                if tile == EMPTY or tile == BOMB:
                    pygame.draw.rect(SURFACE,
                                     (192, 192, 192), rect)
                    if game_over and tile == BOMB:
                        pygame.draw.ellipse(SURFACE,
                                            (225, 225, 0), rect)
                elif tile == OPENED:
                    count = num_of_bomb(field, xpos, ypos)
                    if count > 0:
                        num_image = smallfont.render(
                            "{}".format(count), True, (255, 255, 0))
                        SURFACE.blit(num_image,
                                     (xpos*SIZE+10, ypos*SIZE+10))

        # 선 그리기
        for index in range(0, WIDTH*SIZE, SIZE):
            pygame.draw.line(SURFACE, (96, 96, 96),
                             (index, 0), (index, HEIGHT*SIZE))
        for index in range(0, HEIGHT*SIZE, SIZE):
            pygame.draw.line(SURFACE, (96, 96, 96),
                             (0, index), (WIDTH*SIZE, index))

        if OPEN_COUNT == WIDTH*HEIGHT - NUM_OF_BOMBS:
            SURFACE.blit(message_clear, message_rect.topleft)
            pygame.display.update()
            time.sleep(3)
            
        elif game_over:
            SURFACE.blit(message_over, message_rect.topleft)
            pygame.display.update()
            time.sleep(3)

        score_image = smallfont.render("현재점수 : {}".format(OPEN_COUNT), True, (0, 255, 225))
        timer_image = smallfont.render("남은시간 : {:.1f}".format(TIMER*0.1), True, (0, 255, 225))
        SURFACE.blit(score_image, (WIDTH*SIZE+10, 20))
        SURFACE.blit(timer_image, (WIDTH*SIZE+10, 50))
        TIMER -= 1
        if TIMER == 0 :
            game_over = True
            

        pygame.display.update()

        
        FPSCLOCK.tick(10)

####################################################
import pygame_menu

def set_difficulty(value, difficulty):
    global WIDTH, HEIGHT
    if difficulty == 1 :
        WIDTH, HEIGHT = 10, 10
        HEIGHT = 10
    elif difficulty == 2 :
        WIDTH, HEIGHT = 15, 10
    else :
        WIDTH, HEIGHT = 20, 15

def start_the_game():
    global CHECKED
    global OPEN_COUNT
    global SURFACE
    global TIMER
    SURFACE = pygame.display.set_mode([WIDTH*SIZE+300, HEIGHT*SIZE])
    CHECKED = [[0 for _ in range(WIDTH)] for _ in range(HEIGHT)]
    OPEN_COUNT = 0
    TIMER = 1000
    main()

def show_start_menu():
    #print(pygame.font.get_fonts())
    hanfont = pygame.font.SysFont("malgungothic", 30)
    #gamefont = pygame_menu.font.FONT_8BIT
    t = pygame_menu.themes.THEME_BLUE.copy()
    t.widget_font=hanfont
    menu = pygame_menu.Menu("Menu", 500, 300,theme=t)
    menu.add.selector("난이도", [("하", 1),("중", 2),("상", 3)],
                      onchange=set_difficulty)
    menu.add.button("게임 시작", start_the_game)
    menu.add.button("게임 종료", pygame_menu.events.EXIT)
    menu.mainloop(SURFACE)

####################################################

if __name__ == '__main__':
    show_start_menu()
    #main()
