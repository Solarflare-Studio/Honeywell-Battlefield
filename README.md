# Honeywell Documentation
<!--
Honeywell Battlefield MultiTaction
-->
<!-- PROJECT LOGO -->
<br />
<p align="center">
  <a href="https://solarflarestudio.co.uk/">
    <img src="https://i.imgur.com/XbtXTnB.png" alt="logo" width="100%" height="100%">
  </a>

<h3 align="center">Honeywell Battlefield</h3>

<p align="center">
 This is the documentation for the Honeywell Battlefield MultiTaction experience. 
 <br />
 <a href=""><strong>Explore the docs »</strong></a>
 <br />
 <br />
 <a href="">View Demo</a>
 ·
 <a href="https://github.com/Solarflare-Studio/Honeywell-Battlefield/issues">Report Bug</a>
 ·
 <a href="https://github.com/Solarflare-Studio/Honeywell-Battlefield/issues">Request Feature</a>
</p>



<!-- TABLE OF CONTENTS -->
<details open="open">
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#showcase">Showcase</a></li>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#installation">Installation</a></li>
        <li><a href="#things-to-know">Things To Know</a></li>
      </ul>
    </li>
    <li><a href="#steps-to-use">Steps To Use</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#contact">Contact</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT 1-->
## About The Project
<p align="center">
   <img width="100%" src="https://i.imgur.com/jEtlV90.png">
</p>


### Showcase
<p align="center">
   <img width="100%" src="https://i.imgur.com/hXkf3ox.gif">
</p>

This project was created to showcase the various helicopters that are manufactured by Honeywell. This experience was created many years ago for a MultiTaction table. Thankfully the code and application still function as of unity version `2018.4.3f1`.

Tech Used:
* Unity - Used for developing the games
* C# - Used as the language for development

### Built With

These are all the frameworks & programming languages that were used
* [Unity](https://unity.com/)
* [C#](https://docs.microsoft.com/en-us/dotnet/csharp/)


<!-- GETTING STARTED -->
## Getting Started

Below will show you how to get the experience up and running on your local machine from this repository

### Installation

1. [Download](https://github.com/Solarflare-Studio/Honeywell-Battlefield/archive/refs/heads/main.zip) the repo
2. Clone the repo
   ```sh
   git clone https://github.com/Solarflare-Studio/Honeywell-Battlefield.git
   ```

### Things to know

#### Project Structure
<p align="center">
   <img width="50%" src="https://i.imgur.com/ahHRdtg.png">
</p>

#### Adding New Marker Content
1) Under `Markers` in `MainScene > MainCanvas > Markers` add a new `MarkerPrefab` from `Assets > _Inition > Resources > MarkerPrefab`
<p align="center">
   <img width="50%" src="https://i.imgur.com/Fs5m7aw.png">
</p>

2) Give the Marker a `Marker Number` & Marker name on `Marker Tag` on the `Marker Controller` script
<p align="center">
   <img width="100%" src="https://i.imgur.com/btcnhSE.png">
</p>

3) On the `Touch Manager` GameObject, under the `Tuio Input` add the Marker ID & Marker tag (As seen in image below)
<p align="center">
   <img width="100%" src="https://i.imgur.com/yVhweyy.png">
</p>

4) Create a folder under `Assets > StreamingAssets > Content` with the name of the Helicopter/Content name. Upload all **IMAGES** here **NOT** videos.
<p align="center">
   <img width="50%" src="https://i.imgur.com/xcl33qC.png">
</p>

5) Under `Assets > StreamingAssets > Content` create a new `.XML` file with the name of the helicopter/content.
<p align="center">
   <img width="50%" src="https://i.imgur.com/myg57BC.png">
</p>

6) Paste the following XML into the newly created `.XML` file.
```XML
<?xml version="1.0" encoding="utf-8"?>  
<Product xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
	<Id>CONTENT_ID</Id>
	<Title>xxx</Title>
	<Subtitle>xxx</Subtitle>
	<Description>xxx</Description>
	<Content>
		<Content>
			<Title>xxx</Title>
			<Body>xxx</Body>
		</Content>
		<Content>
			<Title>xxx</Title>
			<Body>xxx</Body>
		</Content>
		<Content>
			<Title>xxx</Title>
			<Body>xxx</Body>
		</Content>
		<Content>
			<Title>xxx</Title>
			<Body>xxx</Body>
		</Content>
		<Content>
			<Title>xxx</Title>
			<Body>xxx</Body>
		</Content>
	</Content>
	<Video>
		<Title>Launch video</Title>
		<ThumbnailPath>Common/COM_IMG_LANC_VID.jpg</ThumbnailPath>
		<VideoPath>VideoAssets/xxx.mp4</VideoPath>
   </Video>
   <Actions>
	   <Action xsi:type="GalleryAction">
		   <Caption>Image Gallery</Caption>
		   <ImagePaths>
			   <string>xxx/xxx.jpg</string>
			   <string>xxx/xxx.jpg</string>
			   <string>xxx/xxx.jpg</string>
			   <string>xxx/xxx.jpg</string>
		   </ImagePaths>
	   </Action>
	   <Action xsi:type="TechSpecsAction">
		   <Caption>TechSpecs Gallery</Caption>
		   <ImagePaths>
			   <string>xxx/xxx.jpg</string>
			   <string>xxx/xxx.jpg</string>
			   <string>xxx/xxx.jpg</string>
		   </ImagePaths>
	   </Action>
	</Actions>
</Product>
```

7) Replace all the "xxx" with the content name. It is **VITAL** that you put the correct ID, that you created earlier, into the `<Id>` tag. Replacing the string `CONTENT_ID`

8) Go back to `Assets > StreamingAssets > VideoAssets` & Upload all video content that is part of the Helicopter/content being added.
<p align="center">
   <img width="50%" src="https://i.imgur.com/qoXdofO.png">
</p>

9) Test the marker is working.

10) As an extra step you can check the marker loads correctly from your keyboard.
    1) To achieve this. Goto `Assets > _Inition > Scripts Markers > DetectMarkers.cs` & Open the script

    2) From here you can scroll down until you see the method titled `SelectedTestMarker()`

    3) Add a conditional check and assign your marker to show on a key press. The code can be as follows:
       ```csharp
       else if (Input.GetKey(Keycode.Equals))
       {
           marker = markerNum
       }
       ```
       ensuring that you replace `markerNum` with the marker number assigned in the inspector earlier on.

    4) Run the application in the editor. You can now press the keycode and the marker should showup.

## Steps to use
1. Open the unity scene in `Assets > _Inition > Scenes > MainScene`

2. Make the changes to the scene/application

3. Build via `PC, Mac & Linux Standalone`.
<p align="center">
   <img width="100%" src="https://i.imgur.com/n6kAWMB.png">
</p>
<!-- ROADMAP -->

## Roadmap

See the [open issues](https://github.com/Solarflare-Studio/Honeywell-Battlefield/issues) for a list of proposed features (and known issues).


<!-- CONTACT -->
## Contact

1. Tommy Webb (Work) - tommy@solarflarestudio.co.uk
2. Tommy Webb (Personal) - tommygeorgewebb@googlemail.com
3. Solarflare Studio - techadmin@solarflarestudio.co.uk

Project Link: [https://github.com/Solarflare-Studio/MOL-ChildrensActivation](https://github.com/Solarflare-Studio/Honeywell-Battlefield)

[product-screenshot]: https://i.imgur.com/dBBBZPK.png

