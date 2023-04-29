shader_type canvas_item;

uniform float amount = 20.0;
uniform float frequency = 0.0;
uniform float speed = 6.0;
uniform bool flip = false;

void vertex() {
	vec3 world = (MODEL_MATRIX * vec4(VERTEX, 1.0, 1.0)).xyz;
	float phase = (frequency * world.x) + (speed * TIME);
	VERTEX.x += (1.0 * float(!flip) - UV.y * UV.y) * amount * sin(phase);
	//VERTEX.y += (1.0 - UV.y * UV.y * UV.y) * height * (sin(phase)) * 0.25f;
}
