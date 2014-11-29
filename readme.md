logadslstatus
=============

This is a simple script with a helper app to log the details of my ADSL connection over time when I had a problem with my telephone connection. I have a TP-Link 8817 modem in bridge mode hooked up to an Asus RT-N16 running Tomato.

Dependencies:
* cron (i'm running this script in linux periodically using cron)
* wget 
* mono (helper app is written in C#)
* TP-Link 8817 modem

The overall system flow is:

Preparation:
* Log into the router and use your browser's cookie inspector to list cookies for the router's IP address.
* Find cookies C0 (hash of username) and C1 (hash of password), and copy their values.
* Create a cookies.txt file containing these cookies and save it in the script directory.

    # HTTP cookie file.
    [router ip address]	TRUE	/	FALSE	4102358400	C0	[C0 value]
    [router ip address]	TRUE	/	FALSE	4102358400	C1	[C1 value]

* Change the IP address in the modempage variable in getstats-tplink.sh to point to your modem.

Running:
* Run getstats-tplink.sh. This should log into the router, pull down the status page, call the helper app to scrape the connection details from it and append the scraped data as CSV to values.csv.
* If that works, set up a cron job to run this at your desired interval (mine's running every minute).

Files:
* src contains source files.
* staging contains files for the script directory.