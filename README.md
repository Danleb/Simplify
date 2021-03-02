# Simplify

## Syntax

```text
simplify ([formula] | [-f filepath] | [-w])
```

- -f - read formulas from file. Comments are: ";", "//", "#".
- -w - watch mode. Type and check formulas endlessly.

## Formula syntax

```text
formula ::= "(" ( AND | OR )  { formula } ")" |
            "(" NOT formula ")" |
            "(" IMPLIES formula formula ")" |
            "(" IFF formula formula ")" |
            literal

literal ::= "TRUE" | "FALSE" | <variableName>

```
