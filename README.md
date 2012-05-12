###About FocusMeter
__FocusMeter__ is a small tool sitting in the system bar that lets you track productive vs. distracted time while working.
Its purpose is to provide a fast and easy way to switch between __being productive__ and __being distracted__.

It stores the data inside an embedded RavenDB database. As of now, there is no UI for displaying reports and statistics on that data. In the future, we might have a way to export tabular data to excel or sql server to do those kind of statistics because ultimately, that's the whole reason why we have this app in the first place. However, let's track some days/weeks of work first.

###Gettting started
Clone the repository to your local machine and run _build.bat_. After it has finished you should find a zip archive _focusmeter.zip_ in your _release_ folder. This archive contains the binaries you need to run the application.
You probably want to put to move these files to a permanenet location like _C:\FocusMeter\_ and add the file _FocusMeter.exe_ to your autostart...

Once the application is running, you will find a small round icon in your system tray. Its color tells you which state it is in:

 - Gray: you're not working right now
 - Green: you're workind and actually _being productive_
 - Red: _something_ is distracting you from your actual work, maybe a phone-call, someone chatting with you, whatever

So the basic idea is that you right click on the icon and select "Start working" first. Then, you are in _productive_ state. If something is preventing you from being productive, you hit __Ctrl + Enter__ to toggle to _distracted_ state. When the distraction disappears and you can go back to actual work, you hit Ctrl + Enter once again and you will be back in _productive_ state. Alternatively you can also switch between the states by using the context menu of the system tray icon. When you stop working (e.g. in the evening) you click on the menu item "Stop working" and will be back in _not working_ state.

###Contributing
Everyone is welcome to submit pull requests and add functionality as needed. Some kind of Excel export would be nice...