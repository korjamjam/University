""" cave - Copyright 2016 Kenichiro Tanaka  """
import sys
from random import randint
import pygame
from pygame.locals import QUIT, Rect, KEYDOWN, K_LEFT, K_RIGHT

pygame.init()
pygame.key.set_repeat(5, 5)
SURFACE = pygame.display.set_mode((600, 800))
FPSCLOCK = pygame.time.Clock()

def main():
    """ 메인 루틴 """
    walls = 80
    ship_x = 250
    velocity = 0
    score = 0
    slope = randint(1, 6)
    sysfont = pygame.font.SysFont(None, 36)
    ship_image = pygame.image.load("shipv.png")
    bang_image = pygame.image.load("bang.png")
    holes = []
    for ypos in range(walls):
        holes.append(Rect(100, ypos * 10, 400, 10))
    game_over = False

    while True:
        is_space_down = False
        for event in pygame.event.get():
            if event.type == QUIT:
                pygame.quit()
                sys.exit()
            elif event.type == KEYDOWN:
                if not game_over :
                    if event.key == K_LEFT:
                        ship_x -= 1
                    if event.key == K_RIGHT:
                        ship_x += 1
                    
        # 내 캐릭터를 이동
        if not game_over:
            score += 10
            #velocity += -3 if is_space_down else 3
            #ship_y += velocity

            # 동굴을 스크롤
            edge = holes[0].copy()
            test = edge.move(slope, 0)
            if test.left <= 0 or test.right >= 600:
                slope = randint(1, 6) * (-1 if slope > 0 else 1)
                edge.inflate_ip(-10, 0)
            edge.move_ip(slope, -10)
            holes.insert(0,edge)
            del holes[80]
            holes = [y.move(0, 10) for y in holes]

            # 충돌?
            if holes[75].left > ship_x or \
                holes[75].right < ship_x + 50:
                    game_over = True

        # 그리기
        SURFACE.fill((0, 255, 0))
        for hole in holes:
            pygame.draw.rect(SURFACE, (0, 0, 0), hole)
        SURFACE.blit(ship_image, (ship_x, 710))
        score_image = sysfont.render("score is {}".format(score),
                                     True, (0, 0, 225))
        SURFACE.blit(score_image, (400, 20))

        if game_over:
            SURFACE.blit(bang_image, (ship_x-30, 700))

        pygame.display.update()
        FPSCLOCK.tick(80)

if __name__ == '__main__':
    main()
