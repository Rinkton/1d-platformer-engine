from PIL import Image
img = Image.open("map.png")
width, height = img.size
y = 0
x = 0
f = open('result.txt', 'w')
nothing = img.getpixel((0, 0))
block = img.getpixel((1, 0))
player = img.getpixel((2, 0))
i = 0

while y < height:
    while x < width:
        if y != 0 or x > 2:
            if i == 3:
                i = 0
                f.write('\n')
                print(x)
            pixel = img.getpixel((x, y))
            if pixel == block:
                f.write(f"new OneEngine.Objs.Block({x}, {y}), ")
                i+=1
            elif pixel == player:
                f.write(f"new OneEngine.Objs.Player({x}, {y}), ")
                i+=1
        x+=1
    x = 0
    y+=1
f.close()
print("program end")