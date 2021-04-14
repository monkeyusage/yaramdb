def print_help():
    print("This is an in memory key value store")
    args = ["exit|quit|q!", "help|get_all", "set", "get"]
    for arg in args:
        print("> available command ", arg)

def loop(dico):
    cmd = get_cmd(input(">>> "))
    