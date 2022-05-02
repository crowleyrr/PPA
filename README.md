# The Pocket Personal Assistant

The purpose of this application is to streamline productivity by combining the most-highly utilized productivity functions into one application. 
This application utilizes the following three tabs:
- The Task Manager: For things that need to be accomplished but on no specific timeline
- The Event Manager: For things a user needs to be at
- The Reminder Manager: For things that need to be accomplished on a specific timeline

The functionalities of these tabs are described more in-depth below:

## Task Manager
The task manager contains a basic interface where users can view their tasks for today.  These tasks have the option of being pushed to be done the next daoy, 
or to be marked as 'done' once they have been completed.
An additional functionality of the task manager is that at midnight, any of the tasks that had been moved to Tomorrow will be moved back to Today as it is a new day.
Each task item contains the following information: the title, a description, Today boolean, and a primary key that is a GUID.

## Event Manager
The event manager contains a calendar interface where users are able to see upcoming events and view details of those events.  The user can click on one calendar date
or multiple at once to view the event(s).  Selected events then have the option to be deleted if they are no longer needed.  An additional functionality of the event
manager is notifications.  Two notifications are sent to the device for every event: one thirty minutes before the event, and one at the time of the event.
Each event contains the following information: the title, a description, the datetime, and a primary key that is a GUID.

As it makes more sense to utilize an existing calendar implementation, the event manager utilizes the Xamarin plugin X-Calendar as a basis.  This calendar was
developed by MarvinE and the GitHub repository for this can be found [here](https://github.com/ME-MarvinE/XCalendar).  The appropriate license info appears in the code files that utilize this plugin.  The license can also be viewed below:

>MIT License
>
>Copyright (c) 2022 MarvinE
>
>Permission is hereby granted, free of charge, to any person obtaining a copy
>of this software and associated documentation files (the "Software"), to deal
>in the Software without restriction, including without limitation the rights
>to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
>copies of the Software, and to permit persons to whom the Software is
>furnished to do so, subject to the following conditions:
>
>The above copyright notice and this permission notice shall be included in all
>copies or substantial portions of the Software.
>
>THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
>IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
>FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
>AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
>LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
>OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
>SOFTWARE.

## Reminder Manager
The reminder manager contains a minimal interface where users can view upcoming reminders.  These reminders can be either snoozed or marked as done.
Snoozing a reminder pushes back the time by 1 hour.  Notifications are sent to the device at the time the reminder is due.
A reminder object contains the following attributes: a name, a datetime, and a primary key that is a GUID.

# Credits
In creating this application, the developer relied upon the tutorials provided by [James Montemagno](https://github.com/jamesmontemagno).  His YouTube channel
containing applicable tutorial videos can be found [here](https://www.youtube.com/channel/UCENTmbKaTphpWV2R2evVz2A).

The developer additionally relied upon the Xamarin.Forms docs distributed by Microsoft.

# Steps to use this project

# Developer Information
Rachel Crowley is a second-year graduate student at Central Michigan University pursuing a master's in Computer Science.  Her main focus within Computer Science is
development - both web and software.

Any inquiries can be directed to crowleyrr@gmail.com
