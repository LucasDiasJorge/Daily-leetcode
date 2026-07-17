# Sliding Window (Janela Deslizante)

Tutorial e template de referência para resolver problemas do padrão **Sliding Window**, muito comum em problemas de arrays e strings no LeetCode.

## O que é

Sliding Window é uma técnica que mantém uma "janela" (um intervalo contíguo `[left, right]`) sobre um array ou string, e vai expandindo/contraindo essa janela em vez de recalcular tudo do zero a cada posição.

Ela transforma soluções de força bruta O(n²) ou O(n³) (que testam todos os subarrays/substrings) em soluções O(n), porque cada elemento entra e sai da janela no máximo uma vez.

## Quando usar

Sinais de que o problema pode ser resolvido com sliding window:

- O problema pede o **maior/menor subarray ou substring** que satisfaz alguma condição.
- Envolve **substrings/subarrays contíguos** (não subsequências).
- Você consegue responder "a condição continua válida se eu adicionar/remover um elemento da ponta?".
- Todos os valores envolvidos são **não negativos** (para o caso de soma), o que garante que expandir sempre aumenta e contrair sempre diminui.

Exemplos clássicos: *Longest Substring Without Repeating Characters*, *Minimum Size Subarray Sum*, *Longest Repeating Character Replacement*, *Permutation in String*, *Minimum Window Substring*.

## Os dois tipos

### 1. Janela de tamanho fixo

Usada quando o tamanho `k` da janela é dado no enunciado (ex: "subarray de tamanho k").

```csharp
int FixedWindow(int[] nums, int k)
{
    int windowSum = 0;
    int best = int.MinValue;

    for (int right = 0; right < nums.Length; right++)
    {
        windowSum += nums[right];

        if (right >= k - 1)
        {
            best = Math.Max(best, windowSum);
            windowSum -= nums[right - k + 1]; // remove o elemento que sai
        }
    }

    return best;
}
```

### 2. Janela de tamanho variável

Usada quando você não sabe o tamanho da janela de antemão — ela cresce e encolhe conforme uma condição.

```csharp
int VariableWindow(int[] nums, int target)
{
    int left = 0;
    int windowSum = 0;
    int best = int.MaxValue;

    for (int right = 0; right < nums.Length; right++)
    {
        windowSum += nums[right]; // 1. expande a janela

        while (windowSum >= target) // 2. contrai enquanto a condição for satisfeita
        {
            best = Math.Min(best, right - left + 1);
            windowSum -= nums[left];
            left++;
        }
    }

    return best == int.MaxValue ? 0 : best;
}
```

## Passo a passo mental

1. **Expanda** a janela avançando `right` e incorporando `nums[right]` no estado (soma, contagem, set, dicionário de frequências, etc).
2. **Verifique** se a janela violou ou satisfez a condição do problema.
3. **Contraia** a janela avançando `left` enquanto for necessário, removendo `nums[left]` do estado antes de avançar.
4. **Atualize a resposta** (tamanho da janela, soma, contagem de janelas válidas...) no momento certo — geralmente logo antes ou logo depois de contrair.
5. Repita até `right` chegar ao fim do array.

## Template com frequência de caracteres (strings)

Muito usado em problemas como *Longest Substring Without Repeating Characters* e *Minimum Window Substring*:

```csharp
int WindowWithFrequency(string s)
{
    var seen = new Dictionary<char, int>();
    int left = 0;
    int best = 0;

    for (int right = 0; right < s.Length; right++)
    {
        char c = s[right];
        seen[c] = seen.GetValueOrDefault(c, 0) + 1;

        while (seen[c] > 1) // condição inválida: caractere repetido
        {
            char l = s[left];
            seen[l]--;
            left++;
        }

        best = Math.Max(best, right - left + 1);
    }

    return best;
}
```

## Complexidade

- **Tempo:** O(n) — apesar do `while` interno, `left` e `right` juntos andam no máximo `2n` vezes no total (cada ponteiro só se move para frente).
- **Espaço:** O(1) para janelas numéricas simples, ou O(k) quando se usa um `HashSet`/`Dictionary` para rastrear elementos únicos (k = tamanho do alfabeto/conjunto de valores).

## Erros comuns

- Esquecer de **remover** o elemento de `left` do estado antes de incrementar `left`.
- Atualizar a resposta no lugar errado (antes de contrair quando deveria ser depois, ou vice-versa).
- Usar sliding window em problemas de **subsequência** (não contígua) — a técnica só funciona para subarrays/substrings contíguos.
- Não tratar arrays/strings vazias ou `k` maior que o tamanho da entrada.

## Problemas resolvidos nesta pasta

| Problema | Tipo de janela |
|---|---|
| (adicionar conforme forem resolvidos) | |
