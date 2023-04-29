shader_type canvas_item;

uniform vec3 color: source_color = vec3(1, 1, 1);

void fragment() {
	COLOR.rgb = color;
}
