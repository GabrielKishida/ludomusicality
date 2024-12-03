using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PixelizeFeature : ScriptableRendererFeature {
	[SerializeField] private Shader pixelateShader;
	[SerializeField] private int screenHeight = 144;
	[SerializeField] private RenderPassEvent renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;

	private PixelizePass customPass;

	public override void Create() {
		customPass = new PixelizePass(pixelateShader, renderPassEvent, screenHeight);
	}
	public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData) {
		renderer.EnqueuePass(customPass);
	}
}

