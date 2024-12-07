
def check_rules(rules, update):
    for rule in rules:
        try:
            if update.index(rule[0]) > update.index(rule[1]):
                return rule
        except ValueError:
            continue
    return None


def fix_violation(violation, update):
    elem = update.index(violation[0])
    target = update.index(violation[1])
    update.insert(target, update.pop(elem))
    return update


def load(filename):
    with open(filename) as f:
        rules = []
        updates = []

        rou = True
        for line in f:
            l = line.strip()
            if rou and l == "":
                rou = False
            elif rou:
                rules.append(l.split("|", 2))
            else:
                updates.append(l.split(","))

        return rules, updates


def p1(rules, updates):
    out = 0
    for u in updates:
        violation = check_rules(rules, u)
        if violation is None:
            mid = len(u) // 2
            out += int(u[mid])
    return out


def p2(rules, updates):
    out = 0
    for u in updates:
        violation = check_rules(rules, u)
        if violation is not None:
            while violation is not None:
                u = fix_violation(violation, u)
                violation = check_rules(rules, u)
            mid = len(u) // 2
            out += int(u[mid])
    return out


def main():
    rules, updates = load("input.txt")
    print(p1(rules, updates))
    print(p2(rules, updates))


if __name__ == "__main__":
    main()