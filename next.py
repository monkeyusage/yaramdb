from __future__ import annotations
BAD_CMD = "Invalid command"

def print_help():
    print("This is an in memory key value store")
    args = ["exit|quit|q!", "help|get_all", "set", "get"]
    for arg in args:
        print("> available command ", arg)

def get_cmd(line:str)->str:
    cmd = line.split(" ")
    if cmd[0] == "help":
        return "help"
    elif cmd[0] in ["exit", "exit()", "quit", "quit()", "q!"]:
        exit()
    if len(cmd) == 2 and cmd[0] == "get":
        return "get"
    elif len(cmd) == 3 and cmd[0] == "set":
        return "set"
    return "invalid"

def exe_cmd(cmd:str, args:list[str] ,dico:dict)->None:
    def get_fn(dico, key):
        val = dico.get(key)
        print("> ", val)
        return dico
    def set_fn(dico, key, val):
        dico.update({key:val})
        return dico
    def invalid_fn(*args):
        print("> invalid command")
        return dico
    actions = {
        "help":print_help,
        "get":get_fn,
        "set":set_fn,
        "invalid":invalid_fn
    }
    
    action = actions[cmd]
    dico = action(dico, *args)

    return dico

def loop(dico):
    line = input(">>> ")
    args = line.split(" ")[1:]
    cmd = get_cmd(line)
    dico = exe_cmd(cmd, args, dico)
    return dico