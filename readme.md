# LINAS SUCHE

## Brief description

Linas Suche is an immersive AR audio guide, taking visitors of the Oberhessisches Museum in Giessen through a narative-based tour. 
Lina, student living in Giessen, is preparing to write an article about the history of the city, based on her grandmother's stories and memories.
It is a site-specific app and AR installation, deployed localy on iPads in the museum, and only available for viewing in the gallery space. 
App is developed in Unity 2022.2.10 and AR Foundation 5.0.2 

## Context of origin & funding notice

Diese iOS AR App ist entstanden  im Verbundprojekt museum4punkt0 – Digitale Strategien für das Museum der Zukunft,  Teilprojekt imp – participate immersively. Virtual storytelling in the museum. Das  Projekt museum4punkt0 wird gefördert durch die Beauftragte der Bundesregierung für Kultur  und Medien aufgrund eines Beschlusses des Deutschen Bundestages. Weitere Informationen:  www.museum4punkt0.de 
Falls benötigt – Englische Variante: 
This iOS AR App is part of the project museum4punkt0 - Digital Strategies for the  Museum of the Future, sub-project imp – participate immersively. Virtual storytelling in the museum. The project museum4punkt0 is funded by the Federal Government  Commissioner for Culture and the 
Media in accordance with a resolution issued by the German Bundestag (Parliament of the  Federal Republic of Germany). Further information: www.museum4punkt0.de 

![BKM-Logo](https://github.com/museum4punkt0/Object-by-Object/blob/77bba25aa5a7f9948d4fd6f0b59f5bfb56ae89e2/04%20Logos/BKM_Fz_2017_Web_de.gif)
![NeustartKultur](https://github.com/museum4punkt0/Object-by-Object/blob/22f4e86d4d213c87afdba45454bf62f4253cada1/04%20Logos/BKM_Neustart_Kultur_Wortmarke_pos_RGB_RZ_web.jpg)

## About

This repository contains Unity project produced for the Linas Suche app. It is compatible with Unity 2022.3.0f1
Most of scripts are tailor-made for the in-app content (various animations, etc) and do not have much reuse value. These classes are in the *Scripts/CustomAnimationScripts* folder. 

*Scripts/AR* and *Scripts/General* folders contain code which has broader use, containing solutions for aligining GameObjects to multiple Image Targets (*PolyImageTrack.cs*, *PolyImageSubTracker.cs*), aligning gallery space to both loaded WorldMap and detected image targets (*Room.cs*), masking background for animated AR content based on colors sampled from the camera feed (*WallcoloredVertices.cs*) etc.

## Installation 

App is only available on-site in the Oberhessisches Museum in Giessen, deployed locally on several iPads, as it is planned as a site-specific Augmented Reality experience, augmenting physical parts of the exhibition with various digital assets, animations and audio. 

## Use

App is intuitive and self explanatory to use, as visitors are instructed by the AR guide, Lina, to follow the butterfly through the exhibition. The animated butterfly, followed by a trail, leads the visitor and guides their attention, from beginning of the tour to the end.
Here is a video recording of the app being used: (video is in production, we'll update the readme file when URL is available)

## Credits

Oberhessisches Museum, Giessen
Studio Neue Museen, Berlin

## License

Copyright © 2023 Oberhessisches Museum / Studio Neue Museen
GNU General Public License 3