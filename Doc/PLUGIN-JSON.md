```json
// IronLuaLoader 플러그인 설정 파일, "//"로 주석 가능
// 플러그인 정보를 설정하고 플러그인과 같이 로드하면 됨
{
    "LUALOADER": {
        "configure_version": 100,   //포맷 버전, 절대 수정하면 안됨
        "enabled": true             //로드 허용/비허용
    },
    "PLUGIN":{
        "name": "TEST",  //플러그인 이름
        "guid": "368590ed-e3bf-4ae4-9f35-eb196333632c", //플러그인 식별자
        "author": "Sbaoor",                           //개발자
	"describe":["IronLuaLoader 예제 설명"],             //설명
        "version": [2,0,0]                          //플러그인 버전
    },
    "DEPENGING": {
        "Library": [ //Lua에서 사용할 라이브러리
            "dkjson"
        ],
        "Plugins": [], //의존성 플러그인의 식별자를 적음
        "IronLuaLoader": 200 //최소 IronLuaRunner 버전
    }
}
```

📝 PLUGIN["name"] 으로 설정된 이름을 불러온다.

📝 "name": "TEST" 이면，"TEST.net.lua"를 찾아 로딩한다. ilp는 당분간 지원하지 않는다.

📝 PLUGINS["describe"] 으로 플러그인 설명을 불러올 수 있다.

❗ 플러그인 로딩 실패 시 ERRORID를 반환하며, 이하 표에서 확인할 수 있다.

ID | 원인 | 해결 방법
-|-|-
300 | lua 플러그인을 찾을 수 없음. | (Json에서 설정한 이름).net.lua 형식인지 확인한다.
430 | guid 중복됨 | 같은 플러그인이 존재하는지, 다른 플러그인이 같은 guid를 갖고 있는지 확인한다.
400 | 오래된 IronLuaLoader | IronLuaLoader을 업데이트 한다.
