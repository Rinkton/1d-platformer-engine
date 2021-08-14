import imagepixel as ip
path = "map.png"
width, height = ip.getsize(path)
y = 0
x = 0
f = open('result.txt', 'w')
nothing = ip.getpixel((0, 0), path)
block = ip.getpixel((1, 0), path)
player = ip.getpixel((2, 0), path)
i = 0

while y < height:
    while x < width:
        if y != 0 or x > 2:
            if i == 3:
                i = 0
                f.write('\n')
                print(x)
            pixel = ip.getpixel((x, y), path)
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