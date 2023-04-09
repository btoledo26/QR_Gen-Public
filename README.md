# QR_Gen

This project is my senior project and also my first webapp. While it is simple, I used it as an opportunity to learn the basics of webapp development using the ASP.NET MVC framework. When I started this project I had no experience with this framework or the MVC pattern, so throughout the duration of the development and design process I made many readjustments. I worked to ensure that I followed the proper conventions, though I've definitely got more to learn.

## Generate QR Codes for:
* URLs
* Phone numbers
* SMS messages
* Emails
* Contact Data

## QR Code Database
All QR codes are timestamped and stored in a database upon generation along with the form input used to generated them. Currently this is all stored in a local MSSQL database, but it could be migrated to an online database. There is a page dedicated to displaying these database entries, so previously generated QR codes can be viewed.

## QR Code Sharing
Generated QR codes can be sent to others via email along with a message. The emails are sent from the project's email address, so all that's required is the address of the recipient. The email will contain "QR Code" as the subject, your message as the body, and an attached image of the generated QR code. 

## QR Code Color Customization
QR codes can be generated in a few different colors including: 
* Red
* Green
* Blue
* Purple
* Orange

Initially I planned on having a color picker for color selection, but I found that the contrast between the foreground and background of the QR codes can impact how easy it is to scan. This led me to set hard values for the colors to keep it simple, but a more constrained color picker could replace this.
