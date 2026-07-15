# Add Two Numbers

https://leetcode.com/problems/add-two-numbers/description/

## O problema

Dois números são representados por listas encadeadas, um dígito por nó, em
ordem **little-endian** (o dígito menos significativo é o `head`). É preciso
somar os dois números e devolver o resultado também como lista encadeada.

Exemplo: `2 -> 4 -> 3` (342) + `5 -> 6 -> 4` (465) = `7 -> 0 -> 8` (807).

## Abordagem inicial (conversão para `int`)

A primeira versão implementada resolvia o problema em três passos:

1. `TransformToInt` — percorre cada lista e reconstrói o número inteiro.
2. Soma os dois números como `int`.
3. `TransformToListNode` — quebra o resultado em dígitos e monta uma nova
   lista, inserindo cada dígito com `InsertFromGenesis`.

### Bug 1 — `InsertFromGenesis` perdia a lista a cada inserção

```csharp
public void InsertFromGenesis(ref ListNode head, int val) {
    while (head != null) {
        head = head.next;
    }
    head = new ListNode(val);
}
```

O método usava a própria variável `head` (passada por `ref`) para percorrer
a lista até o fim. Isso apaga a referência do chamador: ao final do `while`,
`head` fica `null` (porque foi sendo reatribuído a `head.next` a cada volta),
e a linha seguinte cria um nó novo isolado, descartando tudo que havia sido
inserido antes. Na prática, só o **último** dígito inserido sobrevivia.

Correção: percorrer com uma variável auxiliar (`current`) e só tocar em
`head` quando a lista está vazia:

```csharp
public void InsertFromGenesis(ref ListNode? head, int val) {
    if (head == null) {
        head = new ListNode(val);
        return;
    }
    ListNode current = head;
    while (current.next != null) {
        current = current.next;
    }
    current.next = new ListNode(val);
}
```

### Bug 2 — a conversão para `int` estoura para números grandes

Mesmo corrigido, esse desenho tem um problema estrutural: o enunciado do
LeetCode permite listas com até **100 dígitos** por número. Nenhum tipo
numérico (`int`, `long`, nem `decimal`) comporta um valor desses — a
conversão `TransformToInt` sempre vai transbordar (*overflow*) antes de a
soma sequer acontecer. Ou seja, o algoritmo funciona só para entradas
pequenas, não para o caso geral do problema.

## Solução correta — soma dígito a dígito com `carry`

A solução canônica soma os números da mesma forma que fazemos manualmente
no papel, um dígito por vez, propagando o "vai um" (`carry`), sem nunca
converter a lista para um número:

```csharp
public ListNode AddTwoNumbers(ListNode l1, ListNode l2) {
    ListNode dummyHead = new ListNode(0);
    ListNode curr = dummyHead;
    int carry = 0;

    while (l1 != null || l2 != null || carry != 0) {
        int x = (l1 != null) ? l1.val : 0;
        int y = (l2 != null) ? l2.val : 0;
        int sum = carry + x + y;

        carry = sum / 10;
        curr.next = new ListNode(sum % 10);
        curr = curr.next;

        if (l1 != null) l1 = l1.next;
        if (l2 != null) l2 = l2.next;
    }

    return dummyHead.next;
}
```

Por que essa versão resolve os dois problemas acima:

- **Sem limite de tamanho**: nunca existe um número inteiro gigante em
  memória — cada iteração lida com no máximo dois dígitos (0-9) e um
  `carry` (0 ou 1). Funciona para listas de qualquer tamanho.
- **Sem o bug de inserção**: o resultado é construído com o padrão
  `dummyHead` + `curr`, que é o jeito idiomático de montar uma lista
  encadeada nova. `curr` é um ponteiro que sempre aponta para o
  **último** nó já criado, então inserir é O(1): `curr.next = novoNo; curr = curr.next;`.
  Não há necessidade de percorrer a lista para achar o fim (o erro do
  `InsertFromGenesis` original), nem de tratar "lista vazia" como caso
  especial — o `dummyHead` já resolve isso, e no final descartamos ele
  retornando `dummyHead.next`.
- **Condição de parada correta**: o `while` continua enquanto houver
  dígitos em `l1` **ou** `l2` **ou** ainda restar um `carry` pendente
  (ex.: `5 + 5 = 10`, precisa criar um nó extra `1` mesmo depois de
  `l1` e `l2` terminarem).

### Comparação

| | Conversão para `int` | Soma com `carry` |
|---|---|---|
| Funciona com listas grandes (100+ dígitos) | ❌ (overflow) | ✅ |
| Complexidade de inserção no resultado | O(n) por dígito (percorre até o fim) | O(1) por dígito (`curr`) |
| Precisa de lógica especial para lista vazia | Sim | Não (`dummyHead`) |
| Complexidade total | O(n²) no pior caso | O(max(n, m)) |
