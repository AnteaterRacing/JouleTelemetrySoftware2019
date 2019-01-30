import cv2
import os
from PIL import Image
path = '/Volumes/Jacob/FSAE ML/images/race_dataset/'  # specify path


def vid2img():
    vidcap = cv2.VideoCapture('raceFeed2.mp4')
    success, image = vidcap.read()
    count = 0
    success = True

    while success:
        cv2.imwrite(os.path.join(path, "image%d.jpg" % count), image)
        success, image = vidcap.read()
        print('Read a new frame: ', success)    # indicate if success or not
        count += 1


def resize():
    dir = os.listdir(path)
    for item in dir:
        if os.path.isfile(path+item):
            im = Image.open(path+item)
            f, e = os.path.splitext(path+item)
            imResize = im.resize((700, 394), Image.ANTIALIAS)
            imResize.save(f + '.jpg', 'JPEG', quality=100)


resize()
