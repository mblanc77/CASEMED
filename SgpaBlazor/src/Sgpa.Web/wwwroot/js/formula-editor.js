// Helpers del editor visual de fórmulas (campos calculados). Inserta texto en la posición del cursor del
// textarea y deja el caret después de lo insertado; devuelve el nuevo valor para sincronizar con Blazor.
export function insertAtCursor(el, text, caretOffset) {
    if (!el) return null;
    const start = el.selectionStart ?? el.value.length;
    const end = el.selectionEnd ?? el.value.length;
    el.value = el.value.substring(0, start) + text + el.value.substring(end);
    // caretOffset: dónde dejar el cursor dentro del texto insertado (ej. entre paréntesis). -1 = al final.
    const pos = start + (caretOffset >= 0 ? caretOffset : text.length);
    el.focus();
    el.setSelectionRange(pos, pos);
    return el.value;
}
