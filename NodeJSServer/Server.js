const WebSocket = require('ws');

// 서버 8000번 포트 오픈
const wss = new WebSocket.Server({port:8000}, () => {
    console.log('서버 시작');
});

const userList = [];

wss.on('connection', function connection(ws) {

    ws.clientID = getKey(8);

    ws.on('message', (data) => {    // Message 이벤트
        const jsonData = JSON.parse(data);  // 받은 데이터를 Json 파싱한다.
        console.log('받은 데이터 : ', jsonData);

        wss.clients.forEach((client) =>
        {
            client.send(data);  // 접속한 클라이언트들에게 send 한다.
        });

    });

    userList.push(ws.clientID); // 새로 연결된 클라이언트를 유저 리스트에 추가

    ws.send(JSON.stringify({clientID: ws.clientID})); // 클라이언트에게 임시 유저 이름 전송

    console.log('클라이언트 연결 -  ID', ws.clientID);

});

wss.on('listening', () => {
    console.log('리스닝...');
});

function getKey(length) { // 임의의 랜덤 값 뽑기
    let result = '';
    const characters = 'ABCDEFGHIJKLMNOPQRSTUVWXTZ0123456789';

    for (let i=0; i<length; i++) {
        result += characters.charAt(Math.floor(Math.random() * characters.length));
    }

    return result;
}