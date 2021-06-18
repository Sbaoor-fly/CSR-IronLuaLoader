#TOOLAPIS

```ReadText(string:filepath)```

 - 파일의 내용을 전부 반환한다.

```ReadLines(string:filepath)```

 - 파일의 내용을 전부 반환한다.
 - table 타입으로 줄별로 반환한다.

```WriteText(string:filepath,string:texts)```

 - 파일의 기존 내용을 지우고 새로 덮어쓴다.
 - 파일이 존재하지 않을 시 새로 생성한다.

```AppendText(string:filepath,string:texts)```

 - 파일에 내용을 덧붙인다.
 - 파일이 존재하지 않을 시 새로 생성한다.

```IfFile(string:filepath)```

 - 파일이 존재하는지의 여부를 bool로 반환한다.

```IfDir(string:dirpath)```

 - 디렉터리가 존재하는지의 여부를 bool로 반환한다

```CreateDir(string:dirpath)```

 - 디렉터리를 생성한다.
 - 주의 : 한 번에 하나의 디렉터리만 생성할 수 있다.

```HttpGet(string:url)```

 - 원격 HTTP Get 요청 시작
 - 주의 : 비동기로 작동할 수 없다.

```Schedule(function:func,int:delay,int:cycle)```

 - 작업을 예약한다.
 - delay：실행 딜레이, ms 단위를 사용한다.
 - cycle：반복 횟수
 - Cancel 함수로 작업을 중지할 때 사용할 작업 id를 int로 반환한다.

```Cancel(int:id)```

 - Schedule 함수가 반환한 id의 작업을 중지한다.
 - 성공 여부를 bool로 반환한다.

```TaskRun(function:func)```

 - 함수를 비동기로 실행한다.
 
```TableToJson(table:tb)```

 - table 객체를 json으로 변환한다.
 - 다중 table은 변환할 수 없다.
 - dkjson 또는 cjson을 이용하여 다중 table을 json으로 변환할 수 있다.

```ToInt(string:number)```

 - 문자열을 int(unsigned)로 변환한다.

```NewGuid()```

 - Guid를 생성한다.
