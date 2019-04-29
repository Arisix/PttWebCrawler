# PttWebCrawler

The crawler for the web version of PTT, the largest online community in Taiwan. 

## Usage

```
Usage: greet [OPTIONS]+ message
Greet a list of individuals with an optional message.
If no message is specified, a generic greeting is used.

Options:
  -h, --help                 show this message and exit
  -l                         log file path. Defualt is "./log"
  -b                         target board name. Defualt is "Marginalman"
  -i=VALUE1=>VALUE2          start and end index
  -a                         article id. Defualt is "M.1555662923.A.C9F"
  -v, -?, --version          show program's version number and exit
```

Output would be `BoardName_StartIndex_EndIndex.json` (or `BoardName_ArticleId.json`)

## Example

```
PttWebCrawler.exe -b Marginalman -a M.1555662923.A.C9F
```

```
PttWebCrawler.exe -b Marginalman -i "1000=>2000"
```

## Reference

[ptt-web-crawler](https://github.com/jwlin/ptt-web-crawler) by [jwlin](https://github.com/jwlin)