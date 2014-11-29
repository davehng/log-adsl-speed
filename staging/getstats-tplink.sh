#!/bin/bash
modempage=http://10.1.1.3/status/status_deviceinfo.htm
filename=stats-`date +"%Y-%m-%d-%k-%M-%S"`.html
wget --load-cookies cookies.txt $modempage -O $filename -q
mono ExtractAdslInfo.exe $filename values.csv
rm $filename
