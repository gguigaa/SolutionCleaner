# About

> Designed for DotNet projects

This is a simple project to clean or erase folders .vs, bin, obj and packages from a solution folder. 
It is not too uncommon to Visual Studio crash something and you need to clean the mentioned folders so you can continue your work. 
No, it is not a difficult task, I'm just bored of doing it myself when I have to. So I created this simple project to do it for me.
If you also want to use it, feel free to do so. I hope it makes your life easier as it does mine.

# How to use

## Downloading the project

Download the project from the repository. The code is available for you to tweak it as you like, but if you just want to use it, the .exe file is on "dist" folder.

## Configuring environment variables

Optionally, you can configure the path to the solution folder as an environment variable. This way you can run the program from any folder.
If you don't want to do this, you can just run the program from "dist" folder, or run it from wherever foler you want.

1. Press Windows button and type "Edit environment variables";
2. Open the option Edit the system environment variables
3. Click Environment variables... button
4. There you see two boxes, in System Variables box find path variable
5. Click Edit
6. A window pops up, click New
7. Type the Directory path of your .exe or batch file ( Directory means exclude the file name from path)
8. Click Ok on all open windows and restart the command prompt

## Configure the default options

Optionally, you can set your own default options. The options are:

- Clean .vs folders: YES;
- Clean bin folders: YES;
- Clean obj folders: YES;
- Clean packages folders: NO;

Open the file "defaultConfigurations.json" and set the options as you like.

## Running the program

If you set the environment variable, you can run the program from any folder. Just type "SolutionCleaner" in the command prompt and press enter.
If you prefered not to, you can run the program from the "dist" folder. Or you can call it from any folder you want.

Do not forget to close Visual Studio before running the program.