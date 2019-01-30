import cv2
import os
from PIL import Image
path = '/Volumes/Jacob/FSAE ML/images/race_dataset/'  # specify path
# os.chrdir(path)       # use if video's path is different than this file


def vid2img():
    vidcap = cv2.VideoCapture('raceFeed2.mp4')  # make sure .mp4 is in same directory
    success, image = vidcap.read()
    count = 0
    success = True  # must be true for while loop to start

    while success:
        cv2.imwrite(os.path.join(path, "image%d.jpg" % count), image)
        success, image = vidcap.read()
        print('Read a new frame: ', success)    # indicate if success or not
        count += 1


def resize():
    dir = os.listdir(path)  # points to directory with video
    for item in dir:
        if os.path.isfile(path+item):
            im = Image.open(path+item)
            f, e = os.path.splitext(path+item)
            imResize = im.resize((700, 394), Image.ANTIALIAS)   # 700x394 res
            imResize.save(f + '.jpg', 'JPEG', quality=100)  # append .jpg to end of image file


# vid2img()
# resize()
