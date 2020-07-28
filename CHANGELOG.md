# Changelog

## [1.21.0](https://github.com/ExtendRealityLtd/Zinnia.Unity/compare/v1.20.0...v1.21.0) (2020-07-28)

#### Features

* **Extraction:** add extractors for ObjectFollower.EventData ([2426446](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/24264463278c516b5e81f8e87a95008094056506))
  > A collection of extractors that can extract specific data from the ObjectFollower.EventData.
  > 
  > For example, an Interactable is using Follow Rigidbody. When it is diverged, the event data can be feed into the ObjectFollowerEventDataSourceExtractor to get the grabbing interactor.
* **Process:** add Application.onBeforeRender moment ([5ad7da4](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/5ad7da40a160cc5d151aa0a896f77daf101764e4))
  > The MomentProcessor can now process moments in the Unity `BeforeRender` game loop moment.

#### Bug Fixes

* **Property:** add ability to remove diverged state(s) ([f1f139e](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/f1f139e7fe3b946fb944daeed8d2a12822fcb6ac))
  > There is an issue when an object is released on its diverged state, the state is not removed when it converges. The leftover state causes the object not be able to converge and diverge again.
* **Velocity:** add RelativeTo for velocity offsets ([8c10705](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/8c107054d16bd51188107d412acc2adb5cdf2176))
  > There is an issue when using XRNodeVelocityEstimator on controllers, if the play area is rotated 180 degrees, throwing an interactable forward will cause it to be thrown backward.
  > 
  > The solution is to also consider the velocities relative to the play area.

## [1.20.0](https://github.com/ExtendRealityLtd/Zinnia.Unity/compare/v1.19.0...v1.20.0) (2020-07-11)

#### Features

* **Action:** add ability to set initial value of an Action ([bbc8ee0](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/bbc8ee0bec3f1e8f00d759b065481dbf223db89c))
  > The new InitialValue property allows an Action to have the starting value set in the Unity Editor at edit time. Once the script is started then the InitialValue will be used to set the default state of the Action (but no events will be emitted to denote an action change as technically the state hasn't changed if it is moving to the initial state).
  > 
  > This new InitialValue property is only for use in the UnityEditor at Edit time and cannot be changed at runtime nor can it be set via script. If an initial value is required via creation of an Action by script then simply just need to create the Action and call `Receive(<your-initial-value>)` prior to any event listeners being hooked up. This will simply call the `Receive` method and will emit the relevant events, but as no event listeners should have been registered then this won't make any difference.
  > 
  > An extra method of `ReceiveInitialValue` has also been added that will allow the `Receive` method to be called with the initial set value. Again, this is only useful for when creating the Action via the Unity Inspector as the InitialValue cannot be changed via script.
  > 
  > The `DefaultValue` help text has also been updated to make it more clearer what this property is for as it's not the starting value of the Action, but the value the Action needs to be at to be considered disabled.
* **Extension:** add enum extension/helper methods ([33463ce](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/33463ce4ce4fcccf8578472295774c69e73ae98b))
  > A couple of new Enum helper methods have been added that make getting an enum easier by either being able to provide the index of the enum to return or by getting the enum by string name.
  > 
  > The PointerElementPropertyMutator has been updated to take advantage of this new method.
* **Extension:** add new data type extensions for common calculations ([816dc97](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/816dc972979193fd701f1788af28c0753b33f5b8))
  > A common calculation is finding a fine grain distance between two points (either Vector2 or Vector3) where the tolerance is also given in the same type as opposed to just a simple `float`.
  > 
  > The new Vector2.WithinDistance and Vector3.WithinDistance offer this via the relevant extensions.
  > 
  > Another common calculation is converting euler degrees to signed degrees, such as 270' is actually equivalent to -90'. This helps when doing greater than or less than comparisons as a negative rotation of -90' is less than a rotation of 0' whereas 270' as a number would be greater than 0'.
  > 
  > The `Vector3.UnsignedEulerToSignedEuler` will convert the current Vector3 of euler angles into a Signed Euler (-180' to 180f) using the new `float.GetSignedDegree` which simply converts a Euler angle into the -180' to 180' range.
  > 
  > These are then used to provide new Transform extensions for:
  > 
  > * `Transform.SignedEulerAngles` * `Transform.SignedLocalEulerAngles`
  > 
  > Which simply return the respective Euler or Local Euler angle for the Transform but in this signed format.
* **structure:** provide mechanism to change properties via UnityEvents ([98ae181](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/98ae181d6909a95f38c5266c7555c447b013df7b))
  > Some property types cannot be changed via UnityEvents as they are not supported in the UnityEvent inspector, such as Enums, Vectors and Vector3State.
  > 
  > This has been fixed by adding custom setter methods that can be called via the UnityEvent inspector using primitive types that are supported to allow this data to still be set.
* **Tracking:** add divergable property modifier types ([8538d3f](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/8538d3f435738ec8e5254dc3b18a4efd81978e4a))
  > A new Property Modifier type known as a Divergable Property Modifier has been added that allows a property modifier to know when the target has become diverged from the source in whatever property it is tracking.
  > 
  > Only certain types of modifier can actually ever cause a divergence, such as the RigidbodyVelocity and RigidbodyAngularVelocity because they can make it so the target is not keeping exactly up to date with the source and become diverged somewhat.
  > 
  > So now both RigidbodyVelocity and RigidbodyAngularVelocity have become extensions of the DivergablePropertyModifier and now emit events when the source/target diverge and converge again.
  > 
  > It is also possible to turn off this divergence tracking and it is turned off by default as it adds an additional overhead, which should not be automatically implemented unless the overhead is warranted for the benefits of using the functionality.
* **Tracking:** add proxy emitter for ObjectFollower.EventData ([0da6d51](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/0da6d51802e0ef5975df5620770ea437e516621b))
  > The ObjectFollower.EventData can now be proxied via the new ObjectFollowerEventProxyEmitter and this data can also now be used as an input to the PropertyModifier `Modify` method as this makes it easy to chain Property Modifiers together to have one modifier use its data to call another Property Modifier.
* **Yield:** add ability to emit an event after a yield instruction ([f1e70c8](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/f1e70c8ffad0795ac3d30a47b642071a481e8b10))
  > The new Yield events provide the ability to trigger some action after a yield instruction has completed such as seconds passed or at end of frame.
  > 
  > This can be used in conjunction with the Proxy events to first store the payload in the Proxy then trigger the emit after the yield instruction has completed.

#### Bug Fixes

* **Attribute:** record MinMaxRange value changes on prefab instance ([4617d6c](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/4617d6c0868e319e2edaa8860f556465f3bffe2c))
  > There is an issue where the MinMaxRange control will reset the value back to the previous value when it is used within a prefab.
  > 
  > The solution seems to be to record the prefab instance property modification after the custom FloatRange value has been set through the Supyrb `SetValue` extension, which doesn't set the value via the SerializedProperty because that is not supported in Unity on custom data types.
  > 
  > The issue only seems to present itself when changing the value between varying negative values:
  > 
  > * -0.5 * -0.2 * -0.5 (reverts to 0)

#### Code Refactoring

* **guidelines:** apply coding guidelines to empty classes ([fea537b](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/fea537b56976f1d9f2aa9944c116d2d3fc07d9a4))
  > The coding guidelines state that empty classes should have the brackets on the same line as such:
  > 
  > `class { }`
  > 
  > and not
  > 
  > ``` class { } ```
  > 
  > This has now been applied to the relevant offending files.

## [1.19.0](https://github.com/ExtendRealityLtd/Zinnia.Unity/compare/v1.18.0...v1.19.0) (2020-06-07)

#### Features

* **Mutation:** allow limiting axes on mutator offsets ([6f62f49](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/6f62f49d4fb81cd2bacbe35979e30496c0ea3022))
  > The TransformPositionMutator has a Facing Direction offset that can be provided which is used to offset the forward direction based on the rotation of this optional offset GameObject. It is now possible to determine which axes of that offset should be used when utilizing the rotation data as there are occurrences where not all of the offset rotations are wanted.
  > 
  > The TransformEulerRotationMutator has an Origin offset that can be used to rotate around a different pivot point position. It is now possible to determine which axes of direction should be used from the optional pivot point as sometimes not all axes may be required.
* **Process:** emit event when SourceTragetProcessor source changes ([05c5e21](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/05c5e21c74d045b249a664144c2967c3c87e6e06))
  > The SourceTargetProcessor now emits an event when the ActiveSource being used for the process changes. It also emits the initial value as ActiveSource starts as `null` and when it is first enabled and called then ActiveSource will be changed from `null` and therefore it will emit the event.

#### Bug Fixes

* **Operation:** suppress obsolete warning messages in cache tests ([ee03c55](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/ee03c55a80dd0541d1302a768e7379cb30672f82))
  > The Cache operations have been deprecated but the tests still actively use the components to test them and therefore throws warning messages about the use of deprecated components.
  > 
  > It's right to keep the tests but just suppress the warnings.

## [1.18.0](https://github.com/ExtendRealityLtd/Zinnia.Unity/compare/v1.17.1...v1.18.0) (2020-05-31)

#### Features

* **Observer:** add observable property data types ([2c0ee57](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/2c0ee576cee43e408e79c1fd02c69a03a0dfe6eb))
  > The new ObservableProperty types allow a data type property to be set in the inspector but any change to that property is observed and an appropriate event is raised for the state of the property.
  > 
  > This is a better implementation of the ValueCache components which didn't really offer much other than storing a value via code and raising events when that value was modified. The ValueCache components have now been deprecated with the new ObservableProperty counterparts being a much better choice.
* **Proxy:** expose payload as public property ([e2912f4](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/e2912f4aa26f5419bae11fc856637635b75546d0))
  > The Payload property on the Event Proxy has now been exposed as a serialized public property so it can be set via the inspector or via external code or Unity event. This payload can then be emitted at a later stage by calling the existing `EmitPayload` method.
  > 
  > There is also a `ClearPayload` method for setting the Payload property back to the `default` value.
* **Tracking:** apply rotation property based on angular velocity ([d312e36](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/d312e369e849d0b0bafa40dedf6be0f0c775c91d))
  > The new RotateAroundAngularVelocity component will modify a target's rotation property by rotating around the offset using the angular velocity of the given VelocityTracker as the angles in which to rotate per frame.
  > 
  > This can be used to simulate turning something with the rotation of the controller, such as screwing in a screw.

#### Code Refactoring

* **Extraction:** rearrange property order and add headers ([bb3e92a](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/bb3e92a63f2a51f65bc29b782ec831ac452964a0))
  > The property order has been re-arranged so the properties are not split by the events when the concrete classes add more properties and headers have been added to make the split clearer.

### [1.17.1](https://github.com/ExtendRealityLtd/Zinnia.Unity/compare/v1.17.0...v1.17.1) (2020-05-22)

#### Bug Fixes

* **Extraction:** remove Cleared attribute from ValueExtractor.Source ([adff3f2](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/adff3f2bf0c1d618b610eed2e6fc9677c09dcab1))
  > The Source property from the ValueExtractor is of a generic type and if the Cleared attribute is used then Malimbe will always weave an auto generated Clear method that attempts to set the property to null and this will cause issues in IL2CPP when attempting to clear non nullable types such as RaycastHit.
  > 
  > As there is a manual ClearSource method already that sets Source to `default` then this `Cleared` attribute can simply be removed.

## [1.17.0](https://github.com/ExtendRealityLtd/Zinnia.Unity/compare/v1.16.0...v1.17.0) (2020-05-22)

#### Features

* **Association:** determine association by platform, sdk and model ([1680f63](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/1680f6345635b9c76971c70247dabe12dbb339f8))
  > The PlatformDeviceAssociation component allows the determining of the hardware based on matching patterns of the current platform, the SDK loaded by Unity and the model type.
  > 
  > As all of these are patterns, then they can be used to search for anything or specific types of setup.
  > 
  > The LoadedXrDeviceAssociation has now been deprecated as it offers a subset of this new functionality but isn't as powerful.
* **Conversion:** ability to convert between float and normalized float ([b65cd03](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/b65cd031ba829243d3c3800474f36635e0f5c667))
  > The FloatToNormalizedFloat allows a float value to be converted into a normalized float (between 0f and 1f) and the NormalizedFloatToFloat allows for a normalized float to be converted to a float value based within the range provided.
  > 
  > The FloatToBoolean has also had the min/max limit removed so any float value can be used to check to see if it should be within the positive bounds.
* **Conversion:** option to convert vector2 to signed angle ([fd1084d](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/fd1084d3c5590e0f49c3fe9ed8540af9a8184cbe))
  > The Vector2ToAngle component now has the option of converting to a signed angle in either degrees or radians.
  > 
  > This allows an angle range of -180 degrees to 180 degrees instead of 0 to 360 degrees and this will feed in better to the FloatToBoolean when wanting to know if the angle is between a given range as it is possible to do -20/20 as a range whereas it's not possible to do 340/20 as a range.

#### Bug Fixes

* **Extraction:** override ClearSource method for generics ([6ca9cfb](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/6ca9cfbc275e135489bcac6b681e251a708b0c50))
  > There is an issue with using the Malimbe generated ClearSource method when using it with a generic type property as the actual type may not be a nullable type and the MemberCleared weaved code always tries to set it to `null`.
  > 
  > In this regard, it is better to simply override the method and set the property to `default`. This is not done in Malimbe as providing default as the weaved code is extremely tricky.

## [1.16.0](https://github.com/ExtendRealityLtd/Zinnia.Unity/compare/v1.15.0...v1.16.0) (2020-04-21)

#### Features

* **Pointer:** allow origin transform of event data to be overriden ([74c8d80](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/74c8d802b022ee15c70d0462156870277d0bff35))
  > The Pointer origin was previously always the Transform that the ObjectPointer component was on, but this new field allows an alternative origin to be provided if required.
* **Tracking:** allow previous position to be valid located surface ([6592687](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/6592687e83153ab40d127a80dc47777c2dd7c442))
  > The SurfaceLocator would always not consider a valid location if the previous location was the same as the current location. This new option allows that equality check to be ignored.
  > 
  > Also, the equality threshold is no longer a constant and instead a value that can be changed via the public property.
* **Tracking:** allow properties to be applied even if they are equal ([b636bca](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/b636bca076ca92a719a2638aa8af5e0abb0a961a))
  > The TransformPropertyApplier now has an option to still apply properties even if the properties are equal.

#### Bug Fixes

* **Tracking:** calculate properties correctly if dynamic destination ([f1191e9](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/f1191e9c96999dc89f4bfbaeb26e0ae297b6c93b))
  > This fix will calculate properties correctly if IsTransitionDestinationDynamic is true. Before the fix, it does not honor the ApplyTransformations settings.
  > 
  > This fix is by Ethan Cheung <ethan@fight4dream.com>
  > 
  > Co-authored-by: Ethan Cheung <ethan@fight4dream.com>

## [1.15.0](https://github.com/ExtendRealityLtd/Zinnia.Unity/compare/v1.14.1...v1.15.0) (2020-04-14)

#### Features

* **Cache:** provide cache operations for common data types ([f1ef2f0](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/f1ef2f06c4087f4df30dd7b3eda307c3774c47b8))
  > The Cache operation allows a data type value to be stored in a cache and then an appropriate event is raised when the value is updated.
  > 
  > If the value is considered equal then the Unmodified event is emitted. However, if the values are not equal then the Modified event is emitted.
* **Extraction:** add extractors for SurfaceData and RaycastHit ([dcab0ef](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/dcab0ef42464e7ac1523fad9ec339ec63319d4ce))
  > A collection of extractors that firstly extract the RaycastHit data from a SurfaceData and then a collection of extractors that can extract specific data from the RaycastHit.
  > 
  > This new collection means the existing SurfaceDataCollisionPointExtractor becomes obsolete as it is too specific for the extractor pattern and can be achieved by first extracting the RaycastHit from the SurfaceData and then extracting the RaycastHit.point from the RaycastHit output.
* **Extraction:** consolidate all extractors into 1 base class ([77e6f97](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/77e6f979b6f4e20b9d662257fae5abe4bf573817))
  > (Nearly) all of the extractors now all inherit from a single ValueExtractor class which contains most of the logic ensuring extractors all have a standard API.
  > 
  > There are a couple of extractors that have multiple events for extraction and these don't fit into this model yet so have been left out. These are:
  > 
  > * ObjectDistanceComparatorEventDataExtractor * TransformPropertyApplierEventDataExtractor
  > 
  > These will be updated in the future to provide individual extrators that can then follow the standard Extractor pattern.
  > 
  > All Extractors now also implement `iProcessable` so can all be used with a `MomentProcessor`.
  > 
  > All Extractors also invoke a `Failed` event which is raised when the extractor has failed to extract the value. This is to ensure the `PlayAreaDimensionExtractor` still has the relevant events required and can fit in the standard Extractor pattern. Plus, having a `Failed` event is useful to know when an Extractor has failed.
  > 
  > The `TransformPropertyExtractor` has been renamed to `TransformVector3PropertyExtractor` which originally existed within the same file but now it can simply extend the `Vector3Extrator` but the old extractor had a separate property for storing the result of the extraction called `LastExtractedValue`. This property is still available but it has been deprecated and the `Result` field should now be used to get the extracted value.
* **Extraction:** deprecate Vector2ComponentExtractor ([4103c15](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/4103c15e62ec36e68bc14caa26f4e47298bfdebe))
  > The `Zinnia.Data.Type.Transformation.Conversion.Vector2ToFloat` component does the same job as the `Vector2ComponentExtractor` so there is no need to have both.

#### Bug Fixes

* **Extraction:** ensure extraction cannot be mutated if disabled ([bf6ceef](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/bf6ceef3439a87f296cb797af426f10a7e455735))
  > The main `Extract()` method should do a check to see if the component is active and enabled and if its not then it should force set the `Result` to `null` across all Extractors.
  > 
  > Also, the `RequiresBehaviourState` attribute has been added to the `Extract` methods that allow the data to be passed in via a parameter as they were allowing mutation to the source even when the component was inactive.
  > 
  > Finally, any extractors that didn't have the `Extract` methods that allowed a parameter have been updated to include these methods too.
* **Extraction:** ensure extraction logic order is consistent ([4f77a78](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/4f77a78178cc049320f1eb798154cf599598c906))
  > The TransformPropertyExtractor worked in the opposite way from other extrators where the `Extract` method does all the work and the `DoExtract` method just calls the `Extract` method without any return.
  > 
  > This has now been updated so it follows this standard logic.
* **Extraction:** flip extraction logic for local direction ([2cb589c](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/2cb589c85031bf4993cc62d6680efbb7783c920b))
  > The TransformDirectionExtractor had the `UseLocal` logic the wrong way round. It was returning the global `Vector3.<direction>` if `UseLocal` was true and returning the direction of the `Source` if `UseLocal` was false.
  > 
  > This doesn't make sense because `Vector3.<direction>` _is_ the global direction whereas the direction of the `Source` is technically local to the `Source`.
  > 
  > This is also technically a change that can cause breaks because the logic is now flipped. But rather than do a clever deprecation or anything, it's probably just better to handle complaints as its just fixed by checking (or unchecking) the `UseLocal` property.
* **structure:** apply coding conventions ([008723a](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/008723ac96df09a1f5051fa9293b12b7b2cdee48))
  > The coding conventions in regards to namespace order has now been applied so the default VisualStudio namespace order is applied to all scripts.
  > 
  > Any missing code comments have also been added to any non-test related script.

#### Code Refactoring

* **Extraction:** simplify the InvokeResult method ([441b7c9](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/441b7c93bc41e72c97a87775ced2235ffdeedd46))
  > The ValueExtractor now has a way of dealing with the differences between the TResultElement and TEventElement when the InvokeResult method is called by piping the actual logic into a generic InvokeEvent method meaning each of the concrete classes don't have to repeat the logic.

### [1.14.1](https://github.com/ExtendRealityLtd/Zinnia.Unity/compare/v1.14.0...v1.14.1) (2020-04-01)

#### Bug Fixes

* **Action:** remove unchanged event from being chained in sources ([d17a0c5](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/d17a0c591a42f29fb59646679e65a38790731a6a))
  > Having the ValueUnchanged event in the Sources chain does not make sense as it causes the `any` concept of the Sources to fail because the logic goes:
  > 
  > Has SourceA changed? no, then don't call Target.Receive   but SourceA is unchanged so call Target.Receive Has SourceB changed? no, then don't call Target.Receive   but SourceB is unchanged so call Target.Receive
  > 
  > The above scenario would mean Target is still false, however:
  > 
  > Has SourceA changed? Yes, then call Target.Receive(true)   SourceA is changed so the second receive won't be called Has SourceB changed? No, then don't call Target.Receive   SourceB is unchanged so call Target.Receive(false)
  > 
  > Now Target has gone from true to false causing it to reset its own state.
  > 
  > Really, the Sources should only be used for actual change proxying and not try to do an `any` on unchanged values.

## [1.14.0](https://github.com/ExtendRealityLtd/Zinnia.Unity/compare/v1.13.0...v1.14.0) (2020-03-04)

#### Features

* **Tracking:** link supplement headset cameras to an alias association ([364023b](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/364023b3945d5b2de94caf1fc58884b619c5dfcc))
  > The LinkedAliasAssociationCollection now has an additional parameter that can store a list of other cameras that may have relevance to the tracked alias.
  > 
  > This aids when a HMD is using multiple cameras per eye for example.
* **Tracking:** provide haptic process references for controllers ([e96e439](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/e96e439e5db86fef8ff3faaff76ad1d12fe812ea))
  > The LinkedAliasAssociationCollection now has a reference for the left and right controller haptic processes so any CameraRig configuration can provide the default haptic process required for the appropriate SDK.

## [1.13.0](https://github.com/ExtendRealityLtd/Zinnia.Unity/compare/v1.12.0...v1.13.0) (2020-03-02)

#### Features

* **Action:** add ValueUnchanged event to Action ([8c704be](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/8c704be072ce56167dd275e38099f536ce7c40ac))
  > A ValueUnchanged event has been added to compliment the ValueChanged Action. The new ValueUnchanged event will raise when the Action receives the same value as it is currently holding.
  > 
  > This event can then be used to call EmitActivationState to re-raise the relevant events.
* **Action:** add Vector3 action ([ff534bf](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/ff534bf90046f133b265cd418c7832f182648672))
  > The Vector3 Action raises a Vector3 value in the same way as the Vector2 Action.
* **Extraction:** add Unity Time component extractor ([85941f8](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/85941f8892bd76edb8ffe21c7a479da3396fbde0))
  > The TimeComponentExtractor will extract a specified value from the UnityEngine.Time object and emit the result.
* **Proxy:** add event proxies for Vector2 and Vector3 events ([af49ebd](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/af49ebd967b31391a1eb9f4eba000e925a541ac9))
  > The Vector2 and Vector3 events can now be proxied via the new EventProxyEmitters.

## [1.12.0](https://github.com/ExtendRealityLtd/Zinnia.Unity/compare/v1.11.0...v1.12.0) (2020-02-24)

#### Features

* **Data:** add KeyNotFound event to GameObjectRelations ([b75f607](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/b75f607c461bd04189b24c5c981e11d880efcfab))
  > The KeyNotFound event is raised whenever the GetValue method on the GameObjectRelations component is called but no key can be matched because either the key is not in the dictionary or the index given is out of bounds of the collection.

#### Bug Fixes

* **Tracking:** cache tracker source when OnEnable ([bc6cd5f](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/bc6cd5f64ae3648cee0588f0e6cbaab1bba8c314))
  > This fix will cache velocity tracker for this component to work when the ProxySource is set up in editor time.

## [1.11.0](https://github.com/ExtendRealityLtd/Zinnia.Unity/compare/v1.10.4...v1.11.0) (2019-12-31)

#### Features

* **Action:** add method for action to receive its own default value ([4d7a4ba](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/4d7a4ba2552cec9a7493f6d2a7ec3aebe65a541f))
  > The new `ReceiveDefaultValue` method is a shortcut for the `Receive` method but it simply makes the Action receive its own default value.
  > 
  > This can be used to programmatically get an Action to call its own deactivation event if the Action is already activated without needing to know the type of the concrete action. This is useful when dealing with Actions in their generic abstract form but wanting them to emit their deactivated state for whatever reason, such as a linked GameObject becoming disabled.

### [1.10.4](https://github.com/ExtendRealityLtd/Zinnia.Unity/compare/v1.10.3...v1.10.4) (2019-12-21)

#### Miscellaneous Chores

* **deps:** bump io.extendreality.malimbe from 9.6.4 to 9.6.5 ([2a8ba92](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/2a8ba92a14b9b8b7b1e4a4ad86b18b16a324ab3b))
  > Bumps [io.extendreality.malimbe](https://github.com/ExtendRealityLtd/Malimbe) from 9.6.4 to 9.6.5. - [Release notes](https://github.com/ExtendRealityLtd/Malimbe/releases) - [Changelog](https://github.com/ExtendRealityLtd/Malimbe/blob/master/CHANGELOG.md) - [Commits](https://github.com/ExtendRealityLtd/Malimbe/compare/v9.6.4...v9.6.5)
  > 
  > Signed-off-by: dependabot-preview[bot] <support@dependabot.com>

### [1.10.3](https://github.com/ExtendRealityLtd/Zinnia.Unity/compare/v1.10.2...v1.10.3) (2019-12-21)

#### Miscellaneous Chores

* **deps:** use latest pipeline templates ([abcaccd](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/abcaccd551f29eff7a8505fb50871533efc02bf4))

### [1.10.2](https://github.com/ExtendRealityLtd/Zinnia.Unity/compare/v1.10.1...v1.10.2) (2019-12-21)

#### Miscellaneous Chores

* **deps:** bump io.extendreality.malimbe from 9.6.3 to 9.6.4 ([a66df76](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/a66df76d820ab2142b54a43fc2b989aa75bf2556))
  > Bumps [io.extendreality.malimbe](https://github.com/ExtendRealityLtd/Malimbe) from 9.6.3 to 9.6.4. - [Release notes](https://github.com/ExtendRealityLtd/Malimbe/releases) - [Changelog](https://github.com/ExtendRealityLtd/Malimbe/blob/master/CHANGELOG.md) - [Commits](https://github.com/ExtendRealityLtd/Malimbe/compare/v9.6.3...v9.6.4)
  > 
  > Signed-off-by: dependabot-preview[bot] <support@dependabot.com>

### [1.10.1](https://github.com/ExtendRealityLtd/Zinnia.Unity/compare/v1.10.0...v1.10.1) (2019-12-17)

#### Bug Fixes

* **Rule:** allow StringInListRule to work in Unity 2018.3 ([2d3c71b](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/2d3c71ba00a0365f8004549c8aec2fbce9354da2))
  > The TryGetComponent method being used was not valid in 2018.3 and must have been introduced in 2019.1.
  > 
  > The fix is to fallback to using the Zinnia.Extension version of the TryGetComponent method.

## [1.10.0](https://github.com/ExtendRealityLtd/Zinnia.Unity/compare/v1.9.0...v1.10.0) (2019-12-17)

#### Features

* **Rule:** add rule to check if a string pattern is in a string list ([802e0be](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/802e0bef0f4abb002a1b6a7294b98ec139c84ef9))
  > The StringInListRule allows a string pattern to be specified to match against any string found in an associated String Observable List that is a component on the given GameObject.
  > 
  > This can replace the need to use the AnyComponentTypeRule and creating dummy scripts for tags as the string list can be used as the tag name that is then picked up by the rule.

#### Bug Fixes

* **Process:** delete GameObject containter from composite process test ([fcc2c7d](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/fcc2c7dabea374d24e9f10d9aebb67d06eea5ef8))
  > The CompositeProcessTest was creating a GameObject contianier in one of the tests but not deleting it at the end of the test so it was present until all of the tests had run.
  > 
  > This can cause tests to behave incorrectly, so it has been removed.

## [1.9.0](https://github.com/ExtendRealityLtd/Zinnia.Unity/compare/v1.8.2...v1.9.0) (2019-12-02)

#### Features

* **ObjectPointer:** option to disable destination on no collision ([0eec665](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/0eec66507fe50698ab576cc9360981353899df7d))
  > The ObjectPointer can now disable the destination pointer element if there is no collision data from the RayCast.
  > 
  > Also, the ObjectPointer test was incorrect as the pointer elements were set up incorrectly where the container was assigned to the mesh and vice versa. This has now been corrected and the tests updated to reflect this change.

### [1.8.2](https://github.com/ExtendRealityLtd/Zinnia.Unity/compare/v1.8.1...v1.8.2) (2019-12-02)

#### Miscellaneous Chores

* **deps:** bump io.extendreality.malimbe from 9.6.2 to 9.6.3 ([b9a3195](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/b9a3195c22a90510ab86ede88c461811fb6543a0))
  > Bumps [io.extendreality.malimbe](https://github.com/ExtendRealityLtd/Malimbe) from 9.6.2 to 9.6.3. - [Release notes](https://github.com/ExtendRealityLtd/Malimbe/releases) - [Changelog](https://github.com/ExtendRealityLtd/Malimbe/blob/master/CHANGELOG.md) - [Commits](https://github.com/ExtendRealityLtd/Malimbe/compare/v9.6.2...v9.6.3)
  > 
  > Signed-off-by: dependabot-preview[bot] <support@dependabot.com>

### [1.8.1](https://github.com/ExtendRealityLtd/Zinnia.Unity/compare/v1.8.0...v1.8.1) (2019-11-27)

#### Miscellaneous Chores

* **deps:** bump io.extendreality.malimbe from 9.6.1 to 9.6.2 ([b51db65](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/b51db65b92af6e3aa0273c4b94f1d7ee27e35f47))
  > Bumps [io.extendreality.malimbe](https://github.com/ExtendRealityLtd/Malimbe) from 9.6.1 to 9.6.2. - [Release notes](https://github.com/ExtendRealityLtd/Malimbe/releases) - [Changelog](https://github.com/ExtendRealityLtd/Malimbe/blob/master/CHANGELOG.md) - [Commits](https://github.com/ExtendRealityLtd/Malimbe/compare/v9.6.1...v9.6.2)
  > 
  > Signed-off-by: dependabot-preview[bot] <support@dependabot.com>

## [1.8.0](https://github.com/ExtendRealityLtd/Zinnia.Unity/compare/v1.7.0...v1.8.0) (2019-11-07)

#### Features

* **Data:** add properties to extract float from Vector2 ([1afa98b](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/1afa98b3367b17293c6ffe2a34f78e76e6ff4477))
  > Added extraction of magnitude and sqrMagnitude from Vector2.
* **Data:** add properties to extract float from Vector3 ([0fba595](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/0fba5952001a313792125faa0f394e072349dfac))
  > Added Vector3ToFloat to extract x, y, z, magnitude and sqrMagnitude from Vector3.
* **Tracking:** emit speed and angular speed from VelocityEmitter ([d28c5c7](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/d28c5c76ad283899c4535110becaa60fe181296c))
  > Added the EmitSpeed and EmitAngularSpeed methods to VelocityEmitter.

## [1.7.0](https://github.com/ExtendRealityLtd/Zinnia.Unity/compare/v1.6.1...v1.7.0) (2019-11-05)

#### Features

* **Utility:** provide remaining and elapsed time when requested ([e4be751](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/e4be751fc6764aa38be63fcf9f37c97a50a7cade))
  > The CountdownTimer now has EmitElapsedTime and EmitRemainingTime methods which emit the ElapsedTime and RemainingTime of the timer.

### [1.6.1](https://github.com/ExtendRealityLtd/Zinnia.Unity/compare/v1.6.0...v1.6.1) (2019-11-02)

#### Bug Fixes

* **Collision:** always add/remove the CollisionTrackerDisabledObserver ([6056f14](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/6056f14ddd304a93d4c6a4bf729d2c16bc0de670))
  > The CollisionTrackerDisabledObserver is now always added and removed when a collision occurs rather than before where it was only added or removed if that collision state was being listened to.
  > 
  > The previous way would cause the CollisionTrackerDisabledObserver to either not be added at all or be added multiple times because it was never being removed.

## [1.6.0](https://github.com/ExtendRealityLtd/Zinnia.Unity/compare/v1.5.0...v1.6.0) (2019-10-29)

#### Features

* **Haptics:** create haptic pattern based on AudioSource ([a5dfce7](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/a5dfce74b3e98297826b201e4b3e71946fe00225))
  > This haptic generated reflects the current playing audio even if the audio is looping, delayed, volume/pitch modulated. Since this new implementation shares the same core logic as the existing AudioClipHapticPulser the shared logic has been extracted into an abstract superclass.

## [1.5.0](https://github.com/ExtendRealityLtd/Zinnia.Unity/compare/v1.4.1...v1.5.0) (2019-10-28)

#### Features

* **Tracking:** clear stored SurfaceData when SurfaceLocator enabled ([3b4b95c](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/3b4b95c18b806379a10188036132d55ba2ccf681))
  > The SurfaceData type now has a Clear method which clears down the saved data within the datatype, which is called from within the OnEnable method of the SurfaceLocator to ensure the SurfaceData is clean from any previous state if the component is disabled.

#### Bug Fixes

* **Tracking:** provide correct null check on TargetValidity Rule ([c1ca943](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/c1ca943d4e5769dc9a924bb693815126dd875904))
  > The TargetValiduty null check within the SurfaceLocator would never have been null because the Rule always contains the RuleContainer.
  > 
  > The fix is to ensure the null check is done against the Rule.Interface which can be null if no Rule is provided.

#### Miscellaneous Chores

* **deps:** bump io.extendreality.malimbe from 9.6.0 to 9.6.1 ([ef66393](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/ef663938433d6c142fc9643891805195ebacedd4))
  > Bumps [io.extendreality.malimbe](https://github.com/ExtendRealityLtd/Malimbe) from 9.6.0 to 9.6.1. - [Release notes](https://github.com/ExtendRealityLtd/Malimbe/releases) - [Changelog](https://github.com/ExtendRealityLtd/Malimbe/blob/master/CHANGELOG.md) - [Commits](https://github.com/ExtendRealityLtd/Malimbe/compare/v9.6.0...v9.6.1)
  > 
  > Signed-off-by: dependabot-preview[bot] <support@dependabot.com>

### [1.4.1](https://github.com/ExtendRealityLtd/Zinnia.Unity/compare/v1.4.0...v1.4.1) (2019-10-28)

#### Bug Fixes

* **README.md:** provide more concise release data and update info ([083c14c](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/083c14ca7d0da52227e39f19f5c033623cd725c0))
  > The Releases section has been removed and is now just a simple badge at the top of the README. There has been an additional section in `Getting Started` on how to update the package via the Unity Package Manager.
  > 
  > The links have also been ordered in the order of appearance in the document.

## [1.4.0](https://github.com/ExtendRealityLtd/Zinnia.Unity/compare/v1.3.1...v1.4.0) (2019-10-27)

#### Features

* **Process:** a IProcessable that emits event when processed. ([8c9985c](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/8c9985cd649e3610bee2b25cdad9601eae860bef))
  > Allows to call any no argument methods for a given moment.

### [1.3.1](https://github.com/ExtendRealityLtd/Zinnia.Unity/compare/v1.3.0...v1.3.1) (2019-10-26)

#### Miscellaneous Chores

* **deps:** bump io.extendreality.malimbe from 9.5.3 to 9.6.0 ([126af58](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/126af580666a4d854e7d4cb73621fc888493f970))
  > Bumps [io.extendreality.malimbe](https://github.com/ExtendRealityLtd/Malimbe) from 9.5.3 to 9.6.0. - [Release notes](https://github.com/ExtendRealityLtd/Malimbe/releases) - [Changelog](https://github.com/ExtendRealityLtd/Malimbe/blob/master/CHANGELOG.md) - [Commits](https://github.com/ExtendRealityLtd/Malimbe/compare/v9.5.3...v9.6.0)
  > 
  > Signed-off-by: dependabot-preview[bot] <support@dependabot.com>

## [1.3.0](https://github.com/ExtendRealityLtd/Zinnia.Unity/compare/v1.2.0...v1.3.0) (2019-10-26)

#### Features

* **.github:** use organization .github repository ([9921b22](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/9921b22fa74544308fa1c21d3490748d0c960607))
  > GitHub provides a mechanism where a global organization .github repo can be used as a fallback to provide default community health files instead of repeating the same files across multiple repos.
  > 
  > ExtendRealityLtd now has a `.github` repo which should be used as it provides the correct details for this repo.
  > 
  > The README.md has been updated to provide definitive links to the relevant files.

## [1.2.0](https://github.com/ExtendRealityLtd/Zinnia.Unity/compare/v1.1.1...v1.2.0) (2019-10-25)

#### Features

* **Haptics:** option to allow all haptic processes in list to process ([779103b](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/779103bfa9c3fa636359eb9bb3f18e5a9e92c2d4))
  > The HapticProcessor now has a `CeaseAfterFirstSourceProcessed` option which is defaulted to true to ensure existing default behaviour does not change, which is when the first active Haptic Process is processed then the HapticProcessor would cease to process any further Haptic Processes.
  > 
  > With this new option, this default behaviour can be turned off and all Haptic Processes listed in the HapticProcessor can be processed if they are active in the scene.

#### Bug Fixes

* **Velocity:** provide more appropriate tooltip documentation ([c509ce6](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/c509ce68de8972d5fa1dd82052fda8796256248e))
  > The VelocityTrackerProcessor has had the tooltip documentation updated to make it more appropriate and the code has been tidied up a bit so it is more succinct.

### [1.1.1](https://github.com/ExtendRealityLtd/Zinnia.Unity/compare/v1.1.0...v1.1.1) (2019-10-24)

#### Bug Fixes

* **Data:** stop Vector2ToFloatTest extending MonoBehaviour ([0f0ed28](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/0f0ed28368d37a764e439ee9ef5fc1fc4d3f7737))
  > Tests should not need to extend MonoBehaviour and this was causing a warning to be displayed due to this test extending it. The fix is to simply prevent the test class from extending MonoBehaviour.
* **Tracking:** update exception message in ObscuranceQuery ([fa000c5](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/fa000c5ff90ab5926e2f3fb27d5c952e9b79a00a))
  > The ObscuranceQuery logic was changed so the Target requires either a Collider or if it has a Rigidbody then there must be at least 1 child Collider.
  > 
  > The Exception messages we never updated so it provided inaccurate information if the usage criteria was incorrect.

## [1.1.0](https://github.com/ExtendRealityLtd/Zinnia.Unity/compare/v1.0.6...v1.1.0) (2019-10-22)

#### Features

* **Collection:** allow query operations on list to run when inactive ([95950d7](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/95950d76a1e9318c908d348e86c691ff9901e1ee))
  > The query operations on the ObservableList such as Contains and IsListEmpty were previously encapsulated by the `[RequiresBehaviourState]` tag meaning a list contents check could only be performed if the list component was active in the scene.
  > 
  > This blanket denial of operation is too heavy handed as just because a list object is inactive it shouldn't mean a contains check returns false because the list, even though inactive, still can contain the object to check for.
  > 
  > It is still correct that components shouldn't perform an action when they are disabled, so if a list is queried then it should return the correct result but shouldn't perform any action, such as emitting any appropriate events.
  > 
  > This solution uses the new Behaviour extension `IsValidState` to replicate the `[RequiresBehaviourState]` functionality within the method body to allow the correct return value but to prevent the event actions from occurring.
* **Extension:** determine if Behaviour is in a valid active state ([de34e2f](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/de34e2f4098549b5c0b4179fc07016924b72b375))
  > The IsValidState extension method can be used to determine if a Behaviour is in the appropriate valid active state depending on whether the component is enabled and/or whether the GameObject the component resides on is active within itself or within the scene hierarchy.
  > 
  > This mechanism is a copy of the Malimbe `[RequiresBehaviourState]` tag but can be used inline in methods rather than just a blanket early return from the entire method.
* **Rule:** determine if the rule component state can auto reject ([32238fd](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/32238fd6ca93e9c0f97f87fddcba07a2a53e9133))
  > A new base abstract Rule class has been added that all existing MonoBehaviour rules now extend from. And on this new base Rule is the concept of being able to set whether the rule can automatically reject any request based on the state of the Rule component.
  > 
  > Previously, if a Rule component was disabled or on an inactive GameObject then it would always reject the `Accepts` request, but this isn't always the requirement when using a Rule. The new `AutoRejectStates` enum flag allows a Rule to specify what states the rule can be in to automatically reject any request.
  > 
  > All of the tests have been updated to show this new state selection option in practice.

### [1.0.6](https://github.com/ExtendRealityLtd/Zinnia.Unity/compare/v1.0.5...v1.0.6) (2019-10-20)

#### Build System

* **deps:** bump io.extendreality.malimbe from 9.5.2 to 9.5.3 ([b5dfbb0](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/b5dfbb058ae7664b9438e1cc6d5b6f7c173e4cdb))
  > Bumps [io.extendreality.malimbe](https://github.com/ExtendRealityLtd/Malimbe) from 9.5.2 to 9.5.3. - [Release notes](https://github.com/ExtendRealityLtd/Malimbe/releases) - [Changelog](https://github.com/ExtendRealityLtd/Malimbe/blob/master/CHANGELOG.md) - [Commits](https://github.com/ExtendRealityLtd/Malimbe/compare/v9.5.2...v9.5.3)
  > 
  > Signed-off-by: dependabot-preview[bot] <support@dependabot.com>

#### Miscellaneous Chores

* **deps:** add dependabot configuration ([2611d88](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/2611d885a072579d91a559165ed7015524c0348f))

### [1.0.5](https://github.com/ExtendRealityLtd/Zinnia.Unity/compare/v1.0.4...v1.0.5) (2019-10-20)

#### Bug Fixes

* **Data:** prevent collapsible event drawer from setting scene as dirty ([dfabfb9](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/dfabfb9749b9d77d28796e838ea1a3f6365c043e))
  > The CollapsibleUnityEvent drawer Editor drawer was setting the scene as dirty on first draw of the component which was then causing an error in Unity 2019.1 and above when a prefab utilizing the custom drawer was drawn in the inspector. This is because the prefab cannot be saved if it is coming from a 3rd party Unity package and attempting to set the scene as dirty was causing an attempt to save.
  > 
  > There is no reason for this SetDirty to occur as it provides no required functionality so the line has been removed which should fix the error it was causing.

### [1.0.4](https://github.com/ExtendRealityLtd/Zinnia.Unity/compare/v1.0.3...v1.0.4) (2019-10-20)

#### Miscellaneous Chores

* **deps:** use latest pipeline templates ([e2822b0](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/e2822b017c0196a1c6bd9aaf5cbb8254e6b83f28))

### [1.0.3](https://github.com/ExtendRealityLtd/Zinnia.Unity/compare/v1.0.2...v1.0.3) (2019-10-20)

#### Bug Fixes

* **Attribute:** provide custom grey color for restricted muted option ([54465d2](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/54465d22a7ec3b1c2f748ef13d971b18c8893fae))
  > The `[Restricted]` attribute was using a standard `Color.gray` but this became unreadable when using the Unity professional dark skin. A custom grey color is now being used which is visible and readable in both the professional and personal Unity skin.

#### Documentation

* **CONTRIBUTING:** do not include copyright notices ([703e7b6](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/703e7b6f89a67e32a5c7e8d054ddedb43200f8ef)), closes [/help.github.com/en/articles/github-terms-of-service#6](https://github.com//help.github.com/en/articles/github-terms-of-service/issues/6)
  > Authors will continue to retain the copyright for the code committed but do so under the license stated in the repository as outlined in the [GitHub Terms Of

### [1.0.2](https://github.com/ExtendRealityLtd/Zinnia.Unity/compare/v1.0.1...v1.0.2) (2019-10-15)

#### Bug Fixes

* **dependencies:** update Malimbe to prevent a warning log ([585e00f](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/585e00ff1e2ee0ba743247bd162f067a8a6ce50a))
  > When Zinnia was directly referenced by a project as a UPM package the Malimbe dependency of Zinnia logged multiple warnings. These warnings were resolved by an update to Malimbe, which this change updates to.

### [1.0.1](https://github.com/ExtendRealityLtd/Zinnia.Unity/compare/v1.0.0...v1.0.1) (2019-10-14)

#### Bug Fixes

* **ci:** back to npmjs.com ([d2a8cfb](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/d2a8cfbaf2a8b14d3fbe16d4dea0bbe8394b45dc))
  > GitHub's npm feeds only allow publishing scoped packages, but UPM doesn't support those.

## [1.0.0](https://github.com/ExtendRealityLtd/Zinnia.Unity/compare/v0.0.0...v1.0.0) (2019-10-14)

#### :warning: BREAKING CHANGES :warning:

* Zinnia is now a UPM package and does no longer directly include Malimbe, instead referencing it as a package dependency. Consumers of Zinnia will have to follow the added steps in the README to include Zinnia in their projects. ([220b613](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/220b6131fd83ecb237de7bebe8bab7e0c13ad0ab))

#### Build System

* use Malimbe as dependency package ([220b613](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/220b6131fd83ecb237de7bebe8bab7e0c13ad0ab))
  > The compiled output of the dependency Malimbe was copied directly into this project. Keeping binaries of dependencies in the project only is necessary in environments that don't offer the idea of a package and a package manager. With Unity nowadays coming with the Unity Package Manager (UPM) Zinnia can now become a package, referencing its needed dependency Malimbe as a package dependency.

#### Continuous Integration

* add missing changelog file ([936e9dd](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/936e9ddb94ee155ec0afacefa906ddf4f426e161))
  > The changelog will be automatically be created by the continuous delivery pipeline and it will be part of the released package. Thus consumers of the package need a matching `.meta` file to prevent the Unity Editor from logging warnings.
* implement continuous delivery ([1b3a9ea](https://github.com/ExtendRealityLtd/Zinnia.Unity/commit/1b3a9ea53e30b6ec8b048d135611835b4dfaab26))
  > Since Zinnia is now a UPM package it should automatically be released as one. This change adds automatic creation of UPM packages for Zinnia, including automatic SemVer-styled versioning based on the commit messages. A release is both uploaded to the ExtendReality npm GitHub registry as well as an GitHub release (archived .zip).
