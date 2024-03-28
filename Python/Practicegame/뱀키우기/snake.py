import sys
import random
import pygame
from pygame.locals import QUIT, KEYDOWN, K_LEFT, K_RIGHT, K_UP, K_DOWN, Rect

# Pygame 초기화 및 설정
pygame.init()
pygame.key.set_repeat(5, 5)
SURFACE = pygame.display.set_mode([600 + 300, 600])
FPSCLOCK = pygame.time.Clock()
SCORE = 0
SPEED = 5

GREEN = 0
YELLOW = 1
BLUE = 2

class Snake:
    """ Snake 객체 """
    bodies = []

    def __init__(self, pos):
        self.bodies = [pos]

    def move(self, key):
        global SCORE, SPEED
        
        """ Snake를 1프레임만큼 이동 """
        xpos, ypos = self.bodies[0]
        if key == K_LEFT:
            xpos -= 1
        elif key == K_RIGHT:
            xpos += 1
        elif key == K_UP:
            ypos -= 1
        elif key == K_DOWN:
            ypos += 1
        head = [xpos, ypos]

        # 게임 오버 판정
        is_game_over = head in self.bodies

        # 맵을 벗어난 경우 반대편으로 이동
        if head[0] < 0:
            head[0] = W - 1
        if head[1] < 0:
            head[1] = H - 1
        if head[0] >= W:
            head[0] = 0
        if head[1] >= H:
            head[1] = 0

        self.bodies.insert(0, head)
        
        # 먹이를 먹은 경우 처리
        if head in FOODS:
            i = FOODS.index(head)
            del FOODS[i]
            if TYPES[i] == GREEN:
                # 점수 증가
                SCORE += 100
                # 속도 증가 (500점 단위)
                if SCORE % 500 == 0:
                    SPEED += 5
            elif TYPES[i] == YELLOW:
                # 노란색 아이템인 경우 몸 길이를 1로 수정
                self.bodies = []
                self.bodies.insert(0, head)
            elif TYPES[i] == BLUE:
                # 파란색 아이템인 경우 속도를 5로 초기화하고 꼬리 하나 제거
                SPEED = 5
                self.bodies.pop()
                
            del TYPES[i]
            
            # 새로운 먹이 추가
            add_food(self)
        else:
            self.bodies.pop()

        return is_game_over

    def draw(self):
        """ Snake를 화면에 그린다 """
        for body in self.bodies:
            pygame.draw.rect(SURFACE, (0, 255, 255),
                             Rect(body[0] * 30, body[1] * 30, 30, 30))

# 먹이와 먹이 종류를 저장할 리스트
FOODS = []
TYPES = []
(W, H) = (20, 20)

def add_food(snake):
    """ 임의의 장소에 먹이를 배치 """
    while True:
        pos = [random.randint(0, W - 1), random.randint(0, H - 1)]
        if pos in FOODS or pos in snake.bodies:
            continue
        FOODS.append(pos)
        # 먹이 종류 추가 (GREEN 4/5, YELLOW 1/5, BLUE 1/5 확률)
        TYPES.append(random.choice([GREEN, GREEN, GREEN, GREEN, YELLOW, BLUE]))
        break

def paint(snake, message, score_image, speed_image):
    """ 화면 전체 그리기 """
    SURFACE.fill((0, 0, 0))
    snake.draw()
    
    # 먹이 그리기
    for food in FOODS:
        i = FOODS.index(food)
        colors = [(0, 255, 0), (255, 255, 0), (0, 0, 255)]
        pygame.draw.ellipse(SURFACE, colors[TYPES[i]],
                            Rect(food[0] * 30, food[1] * 30, 30, 30))
    
    # 격자 그리기
    for index in range(20):
        pygame.draw.line(SURFACE, (64, 64, 64),
                         (index * 30, 0), (index * 30, 600))
        pygame.draw.line(SURFACE, (64, 64, 64),
                         (0, index * 30), (600, index * 30))
    
    # 게임 오버 메시지 그리기
    if message != None:
        SURFACE.blit(message, (150, 300))

    # 오른쪽 정보 패널 그리기
    pygame.draw.rect(SURFACE, (128, 128, 128), (600, 0, 900, 600))
    
    # 현재 점수와 속도 그리기
    if score_image != None:
        SURFACE.blit(score_image, (610, 20))

    if speed_image != None:
        SURFACE.blit(speed_image, (610, 60))        
        
    pygame.display.update()

def main():
    """ 메인 루틴 """
    myfont = pygame.font.SysFont(None, 80)
    smallfont = pygame.font.SysFont("새굴림", 36)
    
    key = K_DOWN
    message = None
    game_over = False
    snake = Snake((int(W / 2), int(H / 2)))
    
    # 초기에 먹이 10개 추가
    for _ in range(10):
        add_food(snake)

    while True:
        for event in pygame.event.get():
            if event.type == QUIT:
                pygame.quit()
                sys.exit()
            elif event.type == KEYDOWN:
                key = event.key

        if game_over:
            message = myfont.render("Game Over!", True, (255, 255, 0))
        else:
            game_over = snake.move(key)

        # 현재 점수와 속도 표시를 위한 이미지 생성
        score_image = smallfont.render("현재점수 : {}".format(SCORE), True, (255, 255, 0))
        speed_image = smallfont.render("현재속도 : {}".format(SPEED), True, (0, 255, 255))        
        paint(snake, message, score_image, speed_image)
        
        FPSCLOCK.tick(SPEED)

if __name__ == '__main__':
    main()
