[33mcommit 5e24f567c08834aae3fa764e6676a7db4f840798[m
Author: Michał <zurekseventyseven@gmail.com>
Date:   Fri Nov 4 21:40:19 2016 +0100

    Restored accidentaly deleted readme

[33mcommit ec1b297ed67057c13e94de10366f47578493a4e6[m
Author: Michał <zurekseventyseven@gmail.com>
Date:   Fri Nov 4 21:33:51 2016 +0100

    .gitignore is now working

[33mcommit 178179e1ec6d1180fabc5ad8523e2f3bffa2d453[m
Author: Michał <zurekseventyseven@gmail.com>
Date:   Fri Nov 4 21:30:00 2016 +0100

    Changed:
    - Multithreading in connection. Now it's event-based.
    - Added .gitignore
    - Added network data frame wrapper
    - Merged handling many gamepads
    - Sketched some classes for the future manual test purpose

[33mcommit 6d569a55cf20f30d5fd31807a663eb61d3fde97e[m
Author: AdBan <abaniuszewicz@gmail.com>
Date:   Tue Oct 25 14:09:52 2016 +0200

    Added:
    - multi gamepad support
    - user input in GamepadControll class

[33mcommit 6d838574ff60b6cd5ffb92ff8d953a46089ac21d[m
Author: AdBan <abaniuszewicz@gmail.com>
Date:   Mon Oct 24 22:04:13 2016 +0200

    Added: (gamepad)
    - turning around
    - nitro and brake changed to A and B buttons
    - 2nd analog support (turning around)
    - lights support

[33mcommit 818686bb35e12b20d5d037528035c2414066f6cf[m
Author: AdBan <abaniuszewicz@gmail.com>
Date:   Mon Oct 24 22:00:23 2016 +0200

    no message

[33mcommit 21341e0f255632ac3d66b5981e5954e3007f625f[m
Author: Michał <zurekseventyseven@gmail.com>
Date:   Mon Oct 24 00:17:30 2016 +0200

    Handled sending data frames on separate thread

[33mcommit 3ef3bbcb6d9a567904b8db3ce365647a444b4779[m
Author: Michał <zurekseventyseven@gmail.com>
Date:   Sun Oct 23 15:30:22 2016 +0200

    -Created generic architecture to controllers
    -Added keybord controller

[33mcommit 7cf799c270318e47a6f276659c1653ade2653bb6[m
Author: Michał <zurekseventyseven@gmail.com>
Date:   Sun Oct 23 13:55:50 2016 +0200

    Fixed OpenTK lib bug

[33mcommit 98d2b4f31226b4ffa10b64a43be3a203c108d613[m
Merge: f67ae9e 90d6cb4
Author: Michał <zurekseventyseven@gmail.com>
Date:   Sun Oct 23 12:32:15 2016 +0200

    Merge pull request #3 from Zuravvski/Gamepad_handling
    
    Gamepad handling

[33mcommit 90d6cb4290a1a19eadf7b78e717bee7c148a0d49[m
Author: AdBan <abaniuszewicz@gmail.com>
Date:   Sun Oct 23 12:12:17 2016 +0200

    Gamepad handling
    
    Includes managing:
    -acceleration
    -brake
    -nitro
    -steering
    -changing speed while steering
    -creating a final frame
    
    TODO:
    -delete MainWindow() method from class

[33mcommit f67ae9e7dea734f97fcf4b1b31af08e17dcb593f[m
Author: AdBan <abaniuszewicz@gmail.com>
Date:   Sun Oct 23 11:50:39 2016 +0200

    Fixed compilation bug

[33mcommit b3516a4ed34bfbe35d5fbf4dd0a4d3e1aa025c19[m
Author: Michał <zurekseventyseven@gmail.com>
Date:   Sat Oct 22 22:41:00 2016 +0200

    Update README.md

[33mcommit 39491e37f672a8c90990f463062e260b6fddae12[m
Author: Michał <zurekseventyseven@gmail.com>
Date:   Sat Oct 22 22:35:29 2016 +0200

    Created README

[33mcommit b6bfce0bd50442842536007baaffc8f525b51de7[m
Author: Michał <zurekseventyseven@gmail.com>
Date:   Sat Oct 22 21:50:30 2016 +0200

    Added logger and started refactoring
    
    -Added capability to collect and store logs
    -Wrapped up the whole thing
    -Started implementing terminal

[33mcommit 11068b553d62e2af03b520588ceea60210582b31[m
Author: Michał <zurekseventyseven@gmail.com>
Date:   Sat Oct 22 16:21:42 2016 +0200

    Initial commit
