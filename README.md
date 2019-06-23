# BizTalk Orchestration Profiler v2.0

[![Build Status](https://travis-ci.org/mahieddine/biztalk-orchestration-profiler.svg?branch=master)](https://travis-ci.org/mahieddine/biztalk-orchestration-profiler)
[![MIT license](https://img.shields.io/badge/License-MIT-blue.svg)](https://lbesson.mit-license.org/)
[![Maintenance](https://img.shields.io/badge/Maintained%3F-yes-green.svg)](https://GitHub.com/Naereen/StrapDown.js/graphs/commit-activity)    

## Goal 
One of the biggest issues when you are a BizTalk developer is that once you put your code outside of the dev environnement you have no idea how it's behaving (how many times we countered situations when an orchestration is very slow on prod and we can't know exactly easily neither why or which shape(s) is/are taking too much time to execute). This tool is here is to provide an answer to that issue. 

## History
This tool is based on an old powerful tool with the same name that wasn't working anymore since BizTalk 2009.

**Enhancements :**

* Added the possibility to profile only a set of particular orchestration instances identified by their InstanceID
* Added BizTalk application filters (the tool wasn't usable when the BizTalk platform has many orchestrations deployed as you can imagine)
* Fixed timezone issues (The tool wasn't working when the system and SQL server were on two different timezones)

**How can i use it ?**  
[Here](https://github.com/mahieddine/biztalk-orchestration-profiler/blob/master/Docs/README.md) you can find a quick usage guide.

![](Docs/Home_Screen.png)

## Contributions: 
We love contributions! There's lots to do, so why not chat with us about what you're interested in doing? Please star/follow the project and let us know about your plans.

Documentation, bug reports, pull requests, and all other contributions are welcome!

To suggest a feature or report a bug: https://github.com/mahieddine/biztalk-orchestration-profiler/issues

If i was working with you and show you this tool that helped you find out what was going wrong in a minute or so, i'm sure you would have bought me coffee.  


[![ko-fi](https://www.ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/Q5Q5XP49)
