# blocks.py - 게임에서 사용되는 블록, 공, 패들 클래스 및 메인 루틴 구현

import sys
import math
import random
import pygame
import time
from pygame.locals import QUIT, KEYDOWN, K_LEFT, K_RIGHT, Rect

# Block 클래스 정의
class Block:
    """ 블록, 공, 패들 오브젝트 """
    def __init__(self, col, rect, speed=0):
        self.col = col
        self.rect = rect
        self.speed = speed
        self.dir = random.randint(-45, 45) + 270

    def move(self):
        """ 공을 움직인다 """
        self.rect.centerx += math.cos(math.radians(self.dir)) * self.speed
        self.rect.centery -= math.sin(math.radians(self.dir)) * self.speed

    def draw(self):
        """ 블록, 공, 패들을 그린다 """
        if self.speed == 0:
            pygame.draw.rect(SURFACE, self.col, self.rect)
        else:
            pygame.draw.ellipse(SURFACE, self.col, self.rect)

def tick():
    """ 프레임별 처리 """
    global BLOCKS, SCORE
    for event in pygame.event.get():
        if event.type == QUIT:
            pygame.quit()
            sys.exit()
        elif event.type == KEYDOWN:
            if event.key == K_LEFT:
                PADDLE.rect.centerx -= 10
            elif event.key == K_RIGHT:
                PADDLE.rect.centerx += 10
    if BALL.rect.centery < 1000:
        BALL.move()
    if BALL2.rect.centery < 1000:
        BALL2.move()
        
    # 블록과 충돌?
    prevlen = len(BLOCKS)
    BLOCKS = [x for x in BLOCKS if not x.rect.colliderect(BALL.rect)]
    if len(BLOCKS) != prevlen:
        BALL.dir *= -1

    # 점수 추가    
    SCORE += (prevlen - len(BLOCKS)) * 100

    prevlen = len(BLOCKS)
    BLOCKS = [x for x in BLOCKS if not x.rect.colliderect(BALL2.rect)]
    if len(BLOCKS) != prevlen:
        BALL2.dir *= -1        

    # 점수 추가    
    SCORE += (prevlen - len(BLOCKS)) * 100

    # 패들과 충돌?
    if PADDLE.rect.colliderect(BALL.rect):
        BALL.dir = 90 + (PADDLE.rect.centerx - BALL.rect.centerx) / PADDLE.rect.width * 80
    if PADDLE.rect.colliderect(BALL2.rect):
        BALL2.dir = 90 + (PADDLE.rect.centerx - BALL2.rect.centerx) / PADDLE.rect.width * 80

    # 벽과 충돌?
    if BALL.rect.centerx < 0 or BALL.rect.centerx > 600:
        BALL.dir = 180 - BALL.dir
    if BALL.rect.centery < 0:
        BALL.dir = -BALL.dir
        BALL.speed = 15
    if BALL2.rect.centerx < 0 or BALL2.rect.centerx > 600:
        BALL2.dir = 180 - BALL2.dir
    if BALL2.rect.centery < 0:
        BALL2.dir = -BALL2.dir
        BALL2.speed = 15

pygame.init()
pygame.key.set_repeat(15, 15)
SURFACE = pygame.display.set_mode((600+300, 800))
FPSCLOCK = pygame.time.Clock()
BLOCKS = []
PADDLE = Block((242, 242, 0), Rect(230, 700, 160, 30))
BALL = Block((242, 242, 0), Rect(300, 400, 20, 20), 10)  # 공속도=10
BALL2 = Block((255, 0, 0), Rect(500, 400, 20, 20), 7)  # 공속도=7
SCORE = 0
CHECK_SCORE = 0
STAGE = 1

def next_stage():
    global STAGE, PADDLE, BALL, BALL2
    
    STAGE += 1
    colors = [(255, 0, 0), (255, 165, 0), (242, 242, 0),
              (0, 128, 0), (128, 0, 128), (0, 0, 250)]    
    for ypos, color in enumerate(colors, start=0):
        for xpos in range(0, 5):
            BLOCKS.append(Block(color, Rect(xpos * 100 + 60, ypos * 50 + 40, 80, 30)))    
    PADDLE = Block((242, 242, 0), Rect(230, 700, 160, 30))
    BALL = Block((242, 242, 0), Rect(300, 400, 20, 20), 10)  # 공속도=10
    BALL2 = Block((255, 0, 0), Rect(500, 400, 20, 20), 7)  # 공속도=7
    
def main():
    global CHECK_SCORE
    """ 메인 루틴 """
    myfont = pygame.font.SysFont(None, 80)
    smallfont = pygame.font.SysFont("새굴림", 36)
    mess_over = myfont.render("Game Over!", True, (255, 255, 0))
    fps = 30
    colors = [(255, 0, 0), (255, 165, 0), (242, 242, 0),
              (0, 128, 0), (128, 0, 128), (0, 0, 250)]
    for ypos, color in enumerate(colors, start=0):
        for xpos in range(0, 5):
            BLOCKS.append(Block(color, Rect(xpos * 100 + 60, ypos * 50 + 40, 80, 30)))

    while True:
        tick()

        if CHECK_SCORE < SCORE and SCORE % 500 == 0:
            if PADDLE.rect.width > 40:
                PADDLE.rect.inflate_ip(-30, 0)
                CHECK_SCORE = SCORE

        SURFACE.fill((0, 0, 0))
        BALL.draw()
        BALL2.draw()        
        PADDLE.draw()
        for block in BLOCKS:
            block.draw()

        if len(BLOCKS) == 0:
            mess_clear = myfont.render("Next Stage " + str(STAGE+1), True, (255, 255, 0))
            SURFACE.blit(mess_clear, (200, 400))
            pygame.display.update()
            time.sleep(3)
            next_stage()
            
        if BALL.rect.centery > 800 and BALL2.rect.centery > 800 and len(BLOCKS) > 0:
            SURFACE.blit(mess_over, (150, 400))

        pygame.draw.rect(SURFACE, (128,128,128), (600,0,900,800))
        score_image = smallfont.render("현재점수 : {}".format(SCORE), True, (255, 255, 0))
        paddle_image = smallfont.render("패들길이 : {}".format(PADDLE.rect.width), True, (0, 255, 255))        
        stage_image = smallfont.render("스테이지 : {}".format(STAGE), True, (255, 0, 255))        
        SURFACE.blit(score_image, (610, 20))
        SURFACE.blit(paddle_image, (610, 60))
        SURFACE.blit(stage_image, (610, 100))

        pygame.display.update()
        FPSCLOCK.tick(fps)

if __name__ == '__main__':
    main()
