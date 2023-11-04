import uvicorn
from typing import Union
from fastapi import FastAPI, HTTPException
from pydantic import BaseModel
import Remote.RemoteLogic as rem

app = FastAPI()


@app.get("/")
async def root():
    return {"message": "Hello World"}


class Item(BaseModel):
    Host: str
    Port: int
    Password: str
    Username: str
    UseSSHKey: Union[bool, None] = None
    RootPassword: Union[str, None] = None
    Command: str

def my403(text):
    return HTTPException(status_code=403, detail=text)


def my401(text):
    return HTTPException(status_code=401, detail=text)


@app.post('/api/connect')
async def Connect(request: Item):
    print(request)

    if request.Host is None:
        return my403('There is no host in the request')
    if request.Port is None:
        return my403('There is no port in the request')
    if request.Username is None:
        return my403('There is no username in the request')
    if request.Command is None:
        return my403('There is no command in the request')

    if (request.UseSSHKey is False or request.UseSSHKey is None) and request.Password is not None:
        return rem.execute_remote_command_pass(request.Host, request.Port, request.Username, request.Password, request.Command, request.RootPassword)
    else:
        if rem.first_connect(request.Host):
            if request.Password is None:
                return my403('It is impossible to establish SSH connect via keys without password for the first time')
            rem.keygen(request.Host, request.Username, request.Password)
        return rem.execute_remote_command_key(request.Host, request.Username, request.Command, request.RootPassword)


if __name__ == "__main__":
    uvicorn.run(app, host="0.0.0.0", port=8000)
