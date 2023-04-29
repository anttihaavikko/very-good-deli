shader_type canvas_item;

uniform float amount = 12;
uniform float rotation_speed = 3;
uniform float offset = 0;
uniform float speed = 0.1;
uniform float peaks = 7;
uniform float time_mod = 1;

void vertex() {
	vec3 world = (MODEL_MATRIX * vec4(VERTEX, 1.0, 1.0)).xyz;
	float phase = sin((TIME * time_mod + offset * world.x) * 100.0 * speed);
	
	float x = UV.x - 0.5;
    float y = UV.y - 0.5;
    float angle = atan(y, x);
	
	float diff = sin(angle * peaks + TIME * time_mod * rotation_speed) * phase;
	
	VERTEX += vec2(x * amount * diff, y * amount * diff);
}
