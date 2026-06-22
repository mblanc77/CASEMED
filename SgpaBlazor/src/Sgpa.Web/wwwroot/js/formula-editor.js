// Helpers del editor visual de fórmulas (campos calculados).

// Inserta texto en la posición del cursor y deja el caret después (o en caretOffset). Devuelve el nuevo valor.
export function insertAtCursor(el, text, caretOffset) {
    if (!el) return null;
    const start = el.selectionStart ?? el.value.length;
    const end = el.selectionEnd ?? el.value.length;
    el.value = el.value.substring(0, start) + text + el.value.substring(end);
    const pos = start + (caretOffset >= 0 ? caretOffset : text.length);
    el.focus();
    el.setSelectionRange(pos, pos);
    return el.value;
}

// Posición del cursor + valor actual (para el autocompletado de campos al tipear '[').
export function getCaret(el) {
    if (!el) return null;
    return { start: el.selectionStart ?? el.value.length, value: el.value };
}

// Reemplaza el rango [start,end) por text y deja el caret después. Para confirmar una sugerencia de autocompletado.
export function replaceRange(el, start, end, text, caretOffset) {
    if (!el) return null;
    el.value = el.value.substring(0, start) + text + el.value.substring(end);
    const pos = start + (caretOffset >= 0 ? caretOffset : text.length);
    el.focus();
    el.setSelectionRange(pos, pos);
    return el.value;
}
